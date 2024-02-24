﻿using InvestTracker.Shared.Abstractions.Authentication;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Time;
using InvestTracker.Shared.Abstractions.Types;
using InvestTracker.Users.Core.Dtos;
using InvestTracker.Users.Core.Entities;
using InvestTracker.Users.Core.Events;
using InvestTracker.Users.Core.Exceptions;
using InvestTracker.Users.Core.Interfaces;
using InvestTracker.Users.Core.Options;
using InvestTracker.Users.Core.Validators;

namespace InvestTracker.Users.Core.Services;

internal sealed class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITimeProvider _timeProvider;
    private readonly IMessageBroker _messageBroker;
    private readonly IRequestContext _requestContext;
    private readonly PasswordResetOptions _passwordResetOptions;
    private readonly IPasswordValidator _passwordValidator;

    public AccountService(IUserRepository userRepository, IAuthenticator authenticator, IPasswordManager passwordManager, 
        ITimeProvider timeProvider, IMessageBroker messageBroker, IRequestContext requestContext, 
        PasswordResetOptions passwordResetOptions, IPasswordValidator passwordValidator)
    {
        _userRepository = userRepository;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _timeProvider = timeProvider;
        _messageBroker = messageBroker;
        _requestContext = requestContext;
        _passwordResetOptions = passwordResetOptions;
        _passwordValidator = passwordValidator;
    }
    
    public async Task SignUpAsync(SignUpDto dto, CancellationToken token)
    {
        var user = await _userRepository.GetAsync(dto.Email, token);
        if (user is not null)
        {
            throw new EmailAlreadyInUseException(dto.Email);
        }

        var now = _timeProvider.Current();
        var validPassword = _passwordValidator.Validate(dto.Password);
        
        user = new User
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            Password = _passwordManager.Secure(validPassword),
            CreatedAt = now,
            IsActive = true,
            Role = new Role(),
            Subscription = new Subscription
            {
                Value = SystemSubscription.StandardInvestor,
                GrantedAt = now,
                ExpiredAt = null
            }
        };

        await _userRepository.CreateAsync(user, token);
        await _messageBroker.PublishAsync(new AccountCreated(user.Id, user.FullName, user.Email, user.Role.Value, user.Subscription.Value, user.Phone));
    }

    public async Task<JsonWebToken> SignInAsync(SignInDto dto, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
        {
            throw new InvalidCredentialsException();
        }

        var user = await _userRepository.GetAsync(dto.Email, token);
        if (user is null)
        {
            throw new UserNotFoundException(dto.Email);
        }

        if (!user.IsActive)
        {
            throw new UserNotActiveException(user.Id);
        }
        
        if (!_passwordManager.Validate(dto.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        var accessToken = _authenticator.CreateToken(user.Id.ToString(), user.Role.Value, user.Subscription.Value);
        accessToken.Email = user.Email;
        
        return accessToken;
    }
    
    public async Task DeleteCurrentUserAccount(DeleteAccountDto dto, CancellationToken token)
    {
        var currentUser = _requestContext.Identity.UserId;
        var user = await _userRepository.GetAsync(currentUser, token);

        if (user is null)
        {
            throw new UserNotFoundException(currentUser);
        }

        if (user.IsActive is false)
        {
            throw new UserNotActiveException(user.Id);
        }

        if (!_passwordManager.Validate(dto.Password, user.Password))
        {
            throw new InvalidPasswordException();
        }
        
        await _userRepository.DeleteAsync(user, token);
        await _messageBroker.PublishAsync(new AccountDeleted(currentUser));
    }

    public async Task ForgotPassword(string email, CancellationToken token)
    {
        var user = await _userRepository.GetAsync(email, token);

        if (user is null)
        {
            return;
        }
        
        var resetPasswordKey = ResetPasswordKey.Create(user.Id);
        var resetPasswordUri = CreateResetPasswordUri(resetPasswordKey);
        
        var now = _timeProvider.Current();
        var expiredAt = now.AddMinutes(_passwordResetOptions.ExpirationMinutes);
        
        if (user.ResetPassword.ExpiredAt is not null && user.ResetPassword.ExpiredAt > now)
        {
            throw new ResetPasswordActionAlreadyInvokedException(expiredAt);
        }

        user.ResetPassword.Key = resetPasswordKey;
        user.ResetPassword.InvokeAt = now;
        user.ResetPassword.ExpiredAt = expiredAt;
        
        await _userRepository.UpdateAsync(user, token);
        await _messageBroker.PublishAsync(new PasswordForgotten(user.Id, resetPasswordUri));
    }

    public async Task ResetForgottenPassword(ResetPasswordDto dto, CancellationToken token)
    {
        var userId = ResetPasswordKey.GetUserId(dto.ResetPasswordKey).ToGuid();
        var user = await _userRepository.GetAsync(userId, token);
        
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        if (user.ResetPassword.ExpiredAt < _timeProvider.Current())
        {
            throw new ResetPasswordKeyExpiredException();
        }
        
        var validPassword = _passwordValidator.Validate(dto.NewPassword, dto.ConfirmNewPassword);
        
        user.Password = _passwordManager.Secure(validPassword);

        await _userRepository.UpdateAsync(user, token);
        await _messageBroker.PublishAsync(new PasswordChanged(user.Id));
    }

    private string CreateResetPasswordUri(string resetPasswordKey) => $"{_passwordResetOptions.RedirectTo}/{resetPasswordKey}";
}