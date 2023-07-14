using InvestTracker.Shared.Abstractions.Authentication;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Time;
using InvestTracker.Users.Core.Dtos;
using InvestTracker.Users.Core.Entities;
using InvestTracker.Users.Core.Events;
using InvestTracker.Users.Core.Exceptions;
using InvestTracker.Users.Core.Interfaces;

namespace InvestTracker.Users.Core.Services;

internal sealed class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITime _time;
    private readonly IMessageBroker _messageBroker;

    public AccountService(IUserRepository userRepository, IAuthenticator authenticator, 
        IPasswordManager passwordManager, ITime time, IMessageBroker messageBroker)
    {
        _userRepository = userRepository;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _time = time;
        _messageBroker = messageBroker;
    }
    
    public async Task SignUpAsync(SignUpDto dto, CancellationToken token)
    {
        var user = await _userRepository.GetAsync(dto.Email, token);
        if (user is not null)
        {
            throw new EmailAlreadyInUseException(dto.Email);
        }

        user = new User
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Email = dto.Email.ToLowerInvariant(),
            Phone = dto.Phone,
            Password = _passwordManager.Secure(dto.Password),
            CreatedAt = _time.Current(),
            IsActive = true,
            Role = null,
            Subscription = new Subscription
            {
                Value = SystemSubscription.StandardInvestor,
                GrantedAt = _time.Current()
            }
        };

        await _userRepository.CreateAsync(user, token);
        await _messageBroker.PublishAsync(new InvestorCreated(user.Id, user.FullName, user.Email));
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

        var accessToken = _authenticator.CreateToken(user.Id.ToString(), user.Role?.Value, user.Subscription?.Value);
        accessToken.Email = user.Email;
        
        return accessToken;
    }
}