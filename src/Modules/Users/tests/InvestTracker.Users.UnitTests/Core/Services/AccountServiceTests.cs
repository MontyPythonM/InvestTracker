using InvestTracker.Shared.Abstractions.Authentication;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Time;
using InvestTracker.Users.Core.Interfaces;
using InvestTracker.Users.Core.Services;
using NSubstitute;
using Xunit;

namespace InvestTracker.Users.UnitTests.Core.Services;

public class AccountServiceTests
{
    [Fact]
    public async Task SignInAsync_ThrowInvalidCredentialsException_WhenEmailIsEmpty()
    {
        // arrange
        
        // act
        
        // assert
    }

    [Fact]
    public async Task SignInAsync_ThrowInvalidCredentialsException_WhenPasswordIsEmpty()
    {
        // arrange
        
        // act
        
        // assert
    }
    
    #region Arrange
    
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITime _time;
    private readonly IMessageBroker _messageBroker;
    private readonly IAccountService _accountService;
    
    public AccountServiceTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _authenticator = Substitute.For<IAuthenticator>();
        _passwordManager = Substitute.For<IPasswordManager>();
        _time = Substitute.For<ITime>();
        _messageBroker = Substitute.For<IMessageBroker>();
        
        _accountService = new AccountService(
            _userRepository,
            _authenticator,
            _passwordManager,
            _time,
            _messageBroker);
    }

    #endregion
}