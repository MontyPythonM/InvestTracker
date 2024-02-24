using InvestTracker.Shared.Abstractions.Authentication;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Time;
using InvestTracker.Shared.Abstractions.Types;
using InvestTracker.Users.Core.Dtos;
using InvestTracker.Users.Core.Entities;
using InvestTracker.Users.Core.Events;
using InvestTracker.Users.Core.Exceptions;
using InvestTracker.Users.Core.Interfaces;
using InvestTracker.Users.Core.Options;
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
        _userRepository.GetAsync(Arg.Any<Email>(), CancellationToken.None).ReturnsNull();

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
        var dto = GetSignUpDto(email);
        var user = GetUser();
        user.Email = email;
        
        _userRepository.GetAsync(dto.Email, CancellationToken.None)
            .Returns(new User { Id = Guid.NewGuid(), Email = email });
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.SignUpAsync(dto, CancellationToken.None));

        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmailAlreadyInUseException>();
    }
    
    [Fact]
    public async Task SignUpAsync_ShouldAssignStandardInvestorSubscription_WhenUserRegisterAccount()
    {
        // arrange
        var dto = GetSignUpDto();

        _userRepository.GetAsync(dto.Email, CancellationToken.None).ReturnsNull();
        
        // act
        await _accountService.SignUpAsync(dto, CancellationToken.None);

        // assert
        await _userRepository.Received(1).CreateAsync(Arg.Is<User>(u => 
            u.Subscription != null &&
            u.Subscription.Value == SystemSubscription.StandardInvestor), CancellationToken.None);
    }
    
    [Fact]
    public async Task SignUpAsync_ShouldAssignNoneRole_WhenUserRegisterAccount()
    {
        // arrange
        var user = GetUser();
        var dto = GetSignUpDto();
        
        _userRepository.GetAsync(dto.Email, CancellationToken.None).ReturnsNull();

        // act
        await _accountService.SignUpAsync(dto, CancellationToken.None);

        // assert
        await _userRepository.Received(1).CreateAsync(Arg.Is<User>(u => 
            u.Role.Value == SystemRole.None), CancellationToken.None);
        
        user.Role.ShouldBeNull();
    }
    
    [Fact]
    public async Task SignUpAsync_ShouldPublishEvent_WhenUserRegisterAccount()
    {
        // arrange
        var dto = GetSignUpDto();
        
        _userRepository.GetAsync(dto.Email, CancellationToken.None).ReturnsNull();
        
        // act
        await _accountService.SignUpAsync(dto, CancellationToken.None);

        // assert
        await _messageBroker.Received(1).PublishAsync(Arg.Is<AccountCreated>(e =>
            e.FullName == dto.FullName && 
            e.Email == dto.Email));
    }
    #endregion

    #region Arrange
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITimeProvider _timeProvider;
    private readonly IMessageBroker _messageBroker;
    private readonly IAccountService _accountService;
    private readonly IRequestContext _requestContext;
    private readonly IPasswordValidator _passwordValidator;
    
    public AccountServiceTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _authenticator = Substitute.For<IAuthenticator>();
        _passwordManager = Substitute.For<IPasswordManager>();
        _timeProvider = Substitute.For<ITimeProvider>();
        _messageBroker = Substitute.For<IMessageBroker>();
        _requestContext = Substitute.For<IRequestContext>();
        
        _accountService = new AccountService(
            _userRepository,
            _authenticator,
            _passwordManager,
            _timeProvider,
            _messageBroker,
            _requestContext,
            new PasswordResetOptions
            {
                ExpirationMinutes = 1,
                RedirectTo = "http://redirect-to.com"
            },
            _passwordValidator);
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

    private static SignUpDto GetSignUpDto(string email = "default@email.com") 
        => new()
        {
            Email = email,
            FullName = "fullname",
            Password = "password",
            Phone = null
        };

    #endregion
}