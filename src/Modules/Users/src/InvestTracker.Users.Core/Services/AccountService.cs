using InvestTracker.Shared.Abstractions.Authentication;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Time;
using InvestTracker.Shared.Abstractions.Types;
using InvestTracker.Shared.Infrastructure.Authentication;
using InvestTracker.Users.Core.Dtos;
using InvestTracker.Users.Core.Entities;
using InvestTracker.Users.Core.Events;
using InvestTracker.Users.Core.Exceptions;
using InvestTracker.Users.Core.Interfaces;
using InvestTracker.Users.Core.Options;
using InvestTracker.Users.Core.Validators;
using RefreshToken = InvestTracker.Users.Core.Entities.RefreshToken;

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
    private readonly AuthOptions _authOptions;

    public AccountService(IUserRepository userRepository, IAuthenticator authenticator, IPasswordManager passwordManager, 
        ITimeProvider timeProvider, IMessageBroker messageBroker, IRequestContext requestContext, 
        PasswordResetOptions passwordResetOptions, IPasswordValidator passwordValidator, AuthOptions authOptions)
    {
        _userRepository = userRepository;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _timeProvider = timeProvider;
        _messageBroker = messageBroker;
        _requestContext = requestContext;
        _passwordResetOptions = passwordResetOptions;
        _passwordValidator = passwordValidator;
        _authOptions = authOptions;
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

    public async Task<AuthenticationResponse> SignInAsync(SignInDto dto, CancellationToken token)
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

        var accessToken = _authenticator.CreateAccessToken(user.Id, user.Email.Value, user.Role.Value, user.Subscription.Value);

        user.LastSuccessfulLogin = _timeProvider.Current();
        user.RefreshToken = null;
        
        if (_authOptions.UseRefreshToken)
        {
            var refreshToken = _authenticator.CreateRefreshToken();
            user.RefreshToken = new RefreshToken { Token = refreshToken.Token, ExpiredAt = refreshToken.ExpiredAt };
        }
        
        await _userRepository.UpdateAsync(user, token);

        return _authOptions.UseRefreshToken
            ? new AuthenticationResponse(accessToken, new RefreshTokenDto(user.RefreshToken!.Token, user.RefreshToken.ExpiredAt))
            : new AuthenticationResponse(accessToken, null);
    }
    
    public async Task DeleteCurrentUserAccountAsync(DeleteAccountDto dto, CancellationToken token)
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

    public async Task ForgotPasswordAsync(string email, CancellationToken token)
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
        
        if (user.ResetPassword?.ExpiredAt is not null && user.ResetPassword.ExpiredAt > now)
        {
            throw new ResetPasswordActionAlreadyInvokedException(expiredAt);
        }

        user.ResetPassword = new ResetPassword
        {
            Key = resetPasswordKey,
            ExpiredAt = expiredAt,
            InvokeAt = now
        };

        await _userRepository.UpdateAsync(user, token);
        await _messageBroker.PublishAsync(new PasswordForgotten(user.Id, resetPasswordUri));
    }

    public async Task ResetForgottenPasswordAsync(ResetPasswordDto dto, CancellationToken token)
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

    public async Task<AuthenticationResponse> RefreshTokenAsync(string? refreshToken, CancellationToken token)
    {
        if (!_authOptions.UseRefreshToken)
            throw new RefreshTokenDisabledException();
        
        if (string.IsNullOrWhiteSpace(refreshToken))
            throw new InvalidRefreshTokenException();
        
        var user = await _userRepository.GetByRefreshTokenAsync(refreshToken, token);
        
        if (user is null)
            throw new CannotFindUserByRefreshTokenException(refreshToken);

        if (user.RefreshToken!.ExpiredAt <= _timeProvider.Current())
            throw new RefreshTokenExpiredException();
        
        var newAccessToken = _authenticator.CreateAccessToken(user.Id, user.Email, user.Role.Value, user.Subscription.Value);
        var newRefreshToken = _authenticator.CreateRefreshToken();

        user.RefreshToken = new RefreshToken 
        { 
            Token = newRefreshToken.Token, 
            ExpiredAt = newRefreshToken.ExpiredAt
        };

        await _userRepository.UpdateAsync(user, token);
        
        return new AuthenticationResponse(newAccessToken, new RefreshTokenDto(user.RefreshToken.Token, user.RefreshToken.ExpiredAt));
    }

    public async Task RevokeRefreshTokenAsync(Guid userId, CancellationToken token)
    {
        var user = await _userRepository.GetAsync(userId, token);
        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
        user.RefreshToken = null;
        await _userRepository.UpdateAsync(user, token);
    }

    private string CreateResetPasswordUri(string resetPasswordKey) => $"{_passwordResetOptions.RedirectTo}/{resetPasswordKey}";
}