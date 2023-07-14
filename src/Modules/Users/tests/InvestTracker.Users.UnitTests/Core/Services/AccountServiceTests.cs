using InvestTracker.Shared.Abstractions.Authentication;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Time;
using InvestTracker.Shared.Infrastructure.Types;
using InvestTracker.Users.Core.Dtos;
using InvestTracker.Users.Core.Entities;
using InvestTracker.Users.Core.Exceptions;
using InvestTracker.Users.Core.Interfaces;
using InvestTracker.Users.Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using Xunit;

namespace InvestTracker.Users.UnitTests.Core.Services;

public class AccountServiceTests
{
    #region SignInAsync tests
    [Theory]
    [InlineData("", "secret-password")]
    [InlineData("email@email.com", "")]
    public async Task SignInAsync_ShouldThrowInvalidCredentialsException_WhenAnyCredentialIsEmpty(string email, string password)
    {
        // arrange
        var dto = new SignInDto(email, password);
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.SignInAsync(dto, CancellationToken.None));

        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidCredentialsException>();
    }

    [Fact]
    public async Task SignInAsync_ShouldThrowUserNotFoundException_WhenUserNotExist()
    {
        // arrange
        _userRepository.GetAsync(Arg.Any<string>(), CancellationToken.None).ReturnsNull();

        var dto = new SignInDto("email@email.com", "password");
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.SignInAsync(dto, CancellationToken.None));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotFoundException>();
    }
    
    [Fact]
    public async Task SignInAsync_ShouldThrowUserNotActiveException_WhenUserIsInactive()
    {
        // arrange
        var user = GetUser();
        user.IsActive = false;
        
        _userRepository.GetAsync(user.Email, CancellationToken.None).Returns(user);

        var dto = new SignInDto(user.Email, user.Password);

        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.SignInAsync(dto, CancellationToken.None));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotActiveException>();
    }

    [Fact]
    public async Task SignInAsync_ShouldThrowInvalidCredentialsException_WhenPasswordHashesAreNotEquals()
    {
        // arrange
        var user = GetUser();
        user.Password = "valid-user-password";
        
        var dto = new SignInDto(user.Email, "invalid-user-password");
        
        _userRepository.GetAsync(user.Email, CancellationToken.None).Returns(user);
        _passwordManager.Validate(dto.Password, "hashed-invalid-password").Returns(false);

        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.SignInAsync(dto, CancellationToken.None));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidCredentialsException>();
    }
    
    #endregion

    #region SignUpAsync tests

    [Fact]
    public async Task SignUpAsync_ShouldThrowEmailAlreadyInUseException_WhenOtherUserUseEmail()
    {
        // arrange
        const string email = "email@email.com";
        
        var user = GetUser();
        user.Email = email;
        
        var dto = new SignUpDto
        {
            Email = email,
            FullName = "fullname",
            Password = "password",
            Phone = null
        };

        _userRepository.GetAsync(email, CancellationToken.None)
            .Returns(new User { Id = Guid.NewGuid(), Email = email });
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.SignUpAsync(dto, CancellationToken.None));

        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmailAlreadyInUseException>();
    }

    // TODO: complete the SignUpAsync tests
    
    [Fact]
    public async Task SignUpAsync_ShouldAssignStandardInvestorSubscription_WhenUserRegisterAccount()
    {
        // arrange
        
        // act
        
        // assert
    }
    
    [Fact]
    public async Task SignUpAsync_ShouldNotAssignAnyRole_WhenUserRegisterAccount()
    {
        // arrange
        
        // act
        
        // assert
    }
    
    [Fact]
    public async Task SignUpAsync_ShouldPublishEventAndCreateUserEntity_WhenUserRegisterAccount()
    {
        // arrange
        
        // act
        
        // assert
    }
    #endregion

    
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
    
    private static User GetUser() => new()
    {
        Id = "36FF203C-DD33-492C-9690-35698FE02188".ToGuid(),
        FullName = "Name",
        Email = "email@email.com",
        Password = "secret-password",
        CreatedAt = new DateTime(2023, 7, 13),
        IsActive = true
    };

    #endregion
}