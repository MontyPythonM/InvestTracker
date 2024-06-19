using InvestTracker.Shared.Abstractions.Authentication;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Time;
using InvestTracker.Shared.Abstractions.Types;
using InvestTracker.Shared.Infrastructure.Authentication;
using InvestTracker.Users.Core.Dtos;
using InvestTracker.Users.Core.Entities;
using InvestTracker.Users.Core.Enums;
using InvestTracker.Users.Core.Events;
using InvestTracker.Users.Core.Exceptions;
using InvestTracker.Users.Core.Interfaces;
using InvestTracker.Users.Core.Options;
using InvestTracker.Users.Core.Services;
using InvestTracker.Users.Core.Validators;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using Xunit;
using Role = InvestTracker.Users.Core.Entities.Role;
using Subscription = InvestTracker.Users.Core.Entities.Subscription;

namespace InvestTracker.Users.UnitTests.Core.Services;

public class AccountServiceTests
{
    #region SignInAsync
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

    #region SignUpAsync

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
        var dto = GetSignUpDto();
        
        _userRepository.GetAsync(dto.Email, CancellationToken.None).ReturnsNull();

        // act
        await _accountService.SignUpAsync(dto, CancellationToken.None);

        // assert
        await _userRepository.Received(1).CreateAsync(Arg.Is<User>(u => 
            u.Role.Value == SystemRole.None), CancellationToken.None);
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

    #region DeleteCurrentUserAccountAsync

    [Fact]
    public async Task DeleteCurrentUserAccountAsync_ShouldThrowUserNotFoundException_WhenUserNotExists()
    {
        // arrange
        var dto = new DeleteAccountDto { Password = "password" };

        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.DeleteCurrentUserAccountAsync(dto, CancellationToken.None));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotFoundException>();
    }
    
    [Fact]
    public async Task DeleteCurrentUserAccountAsync_ShouldThrowUserNotActiveException_WhenUserAccountIsNotActive()
    {
        // arrange
        const string correctPassword = "password";
        
        var user = GetUser();
        user.IsActive = false;
        
        var dto = new DeleteAccountDto { Password = correctPassword };

        _requestContext.Identity.UserId.Returns(user.Id);
        _userRepository.GetAsync(user.Id, CancellationToken.None).Returns(user);

        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.DeleteCurrentUserAccountAsync(dto, CancellationToken.None));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotActiveException>();
    }
    
    [Fact]
    public async Task DeleteCurrentUserAccountAsync_ShouldPublishEvent_WhenUserDeleteAccount()
    {
        // arrange
        const string correctPassword = "password";
        
        var user = GetUser();
        user.Password = correctPassword;
        
        var dto = new DeleteAccountDto { Password = correctPassword };

        _requestContext.Identity.UserId.Returns(user.Id);
        _userRepository.GetAsync(user.Id, CancellationToken.None).Returns(user);
        _passwordManager.Validate(Arg.Any<string>(), Arg.Any<string>()).Returns(true);
            
        // act
        await _accountService.DeleteCurrentUserAccountAsync(dto, CancellationToken.None);

        // assert
        await _messageBroker.Received(1).PublishAsync(Arg.Is<AccountDeleted>(e => e.Id == user.Id));
    }

    #endregion
    
    #region ForgotPasswordAsync

    [Fact]
    public async Task ForgotPasswordAsync_ShouldPublishEvent_WhenUserInvokeForgotPasswordAction()
    {
        // arrange
        const string email = "test@test.com";
        var user = GetUser();
        user.Email = email;
        
        _userRepository.GetAsync(email, CancellationToken.None).Returns(user);

        // act
        await _accountService.ForgotPasswordAsync(email, CancellationToken.None);

        // assert
        await _messageBroker.Received(1).PublishAsync(Arg.Is<PasswordForgotten>(e => e.UserId == user.Id));
    }
    
    [Fact]
    public async Task ForgotPasswordAsync_ShouldUpdateUserEntity_WhenUserInvokeForgotPasswordAction()
    {
        // arrange
        const string email = "test@test.com";
        var user = GetUser();
        user.Email = email;
        
        _userRepository.GetAsync(email, CancellationToken.None).Returns(user);

        // act
        await _accountService.ForgotPasswordAsync(email, CancellationToken.None);

        // assert
        await _userRepository.Received(1).UpdateAsync(user, CancellationToken.None);
    }
    
    [Fact]
    public async Task ForgotPasswordAsync_ShouldThrowResetPasswordActionAlreadyInvokedException_WhenResetPasswordActionWasAlreadyInvokedAndItsNotExpired()
    {
        // arrange
        var accountService = new AccountService(_userRepository, _authenticator, _passwordManager,
            _timeProvider, _messageBroker, _requestContext, new PasswordResetOptions(),
            _passwordValidator, new AuthOptions());
        
        const string email = "test@test.com";
        var now = DateTime.Now;
        
        var user = GetUser();
        user.Email = email;
        user.ResetPassword = new ResetPassword
        {
            Key = "reset-password-key",
            ExpiredAt = now.AddMinutes(5),
            InvokeAt = now.AddMinutes(-5) 
        };

        _timeProvider.Current().Returns(now);
        _userRepository.GetAsync(email, CancellationToken.None).Returns(user);

        // act
        var exception = await Record.ExceptionAsync(() => 
            accountService.ForgotPasswordAsync(email, CancellationToken.None));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ResetPasswordActionAlreadyInvokedException>();
    }
    
    [Fact]
    public async Task ForgotPasswordAsync_ShouldNotThrowResetPasswordRequiredWaitingException_WhenUserNeverResetHisPassword()
    {
        // arrange
        const string email = "test@test.com";
        
        var user = GetUser();
        user.Email = email;
        user.ResetPassword = null;

        _timeProvider.Current().Returns(new DateTime(2023, 04, 05, 12, 30, 00));
        _userRepository.GetAsync(email, CancellationToken.None).Returns(user);

        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.ForgotPasswordAsync(email, CancellationToken.None));
        
        // assert
        exception.ShouldBeNull();
        exception.ShouldNotBeOfType<ResetPasswordRequiredWaitingException>();
    }
    
    [Fact]
    public async Task ForgotPasswordAsync_ShouldThrowPasswordResetRequiredWaitingException_WhenResetPasswordActionWasAlreadyInvokedAndItsNotExpired()
    {
        // arrange
        const string email = "test@test.com";
        var now = new DateTime(2024, 01, 05, 13, 00, 00);
        
        var user = GetUser();
        user.Email = email;
        user.ResetPassword = new ResetPassword
        {
            Key = "reset-password-key",
            ExpiredAt = now.AddMinutes(-1),
            InvokeAt = now.AddMinutes(-11),
            Counter = 1
        };

        _timeProvider.Current().Returns(now);
        _userRepository.GetAsync(email, CancellationToken.None).Returns(user);

        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.ForgotPasswordAsync(email, CancellationToken.None));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ResetPasswordRequiredWaitingException>();
    }
    
    #endregion
        
    #region ResetForgottenPasswordAsync

    [Fact]
    public async Task ResetForgottenPasswordAsync_ShouldPublishEvent_WhenResetPasswordNotExpired()
    {
        // arrange
        var user = GetUser();
        var resetPasswordKey = ResetPasswordKey.Create(user.Id);
        var newPassword = "password";
        
        user.ResetPassword = new ResetPassword
        {
            Key = resetPasswordKey,
            ExpiredAt = DateTime.Now.AddMinutes(5),
            InvokeAt = DateTime.Now.AddMinutes(-1)
        };

        var dto = new ResetPasswordDto
        {
            ResetPasswordKey = resetPasswordKey,
            NewPassword = newPassword,
            ConfirmNewPassword = newPassword
        };
        
        _userRepository.GetAsync(user.Id, CancellationToken.None).Returns(user);
        
        // act
        await _accountService.ResetForgottenPasswordAsync(dto, CancellationToken.None);

        // assert
        await _messageBroker.Received(1).PublishAsync(Arg.Is<PasswordChanged>(e => e.UserId == user.Id));
    }

    [Fact]
    public async Task ResetForgottenPasswordAsync_ShouldThrowResetPasswordKeyExpiredException_WhenResetPasswordExpired()
    {
        // arrange
        var user = GetUser();
        var resetPasswordKey = ResetPasswordKey.Create(user.Id);
        var newPassword = "password";

        var now = new DateTime(2022, 01, 01);
        _timeProvider.Current().Returns(now);
        
        user.ResetPassword = new ResetPassword
        {
            Key = resetPasswordKey,
            ExpiredAt = now.AddMinutes(-5),
            InvokeAt = now.AddMinutes(-10)
        };

        var dto = new ResetPasswordDto
        {
            ResetPasswordKey = resetPasswordKey,
            NewPassword = newPassword,
            ConfirmNewPassword = newPassword
        };
        
        _userRepository.GetAsync(user.Id, CancellationToken.None).Returns(user);
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.ResetForgottenPasswordAsync(dto, CancellationToken.None));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ResetPasswordKeyExpiredException>();
    }

    [Fact]
    public async Task ResetForgottenPasswordAsync_ShouldThrowResetPasswordKeyExpiredException_WhenUserForgotPasswordButHisResetPasswordKeyExpired()
    {
        // arrange
        var user = GetUser();
        var resetPasswordKey = ResetPasswordKey.Create(user.Id);
        var newPassword = "password";
        var now = new DateTime(2022, 01, 01);

        user.ResetPassword = new ResetPassword
        {
            Key = "key",
            ExpiredAt = now.AddMinutes(-60),
            InvokeAt = now.AddDays(-1),
            Counter = 0
        };

        var dto = new ResetPasswordDto
        {
            ResetPasswordKey = resetPasswordKey,
            NewPassword = newPassword,
            ConfirmNewPassword = newPassword
        };
        
        _timeProvider.Current().Returns(now);
        _userRepository.GetAsync(user.Id, CancellationToken.None).Returns(user);
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.ResetForgottenPasswordAsync(dto, CancellationToken.None));
        
        // assert
        exception.ShouldBeOfType<ResetPasswordKeyExpiredException>();
    }
    
    [Fact]
    public async Task ResetForgottenPasswordAsync_ShouldThrowPasswordAlreadyResetException_WhenUserNotForgotPassword()
    {
        // arrange
        var user = GetUser();
        var resetPasswordKey = ResetPasswordKey.Create(user.Id);
        var newPassword = "password";

        user.ResetPassword = null;

        var dto = new ResetPasswordDto
        {
            ResetPasswordKey = resetPasswordKey,
            NewPassword = newPassword,
            ConfirmNewPassword = newPassword
        };
        
        _userRepository.GetAsync(user.Id, CancellationToken.None).Returns(user);
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.ResetForgottenPasswordAsync(dto, CancellationToken.None));
        
        // assert
        exception.ShouldBeOfType<PasswordAlreadyResetException>();
    }
    
    [Fact]
    public async Task ResetForgottenPasswordAsync_ShouldThrowUserNotFoundException_WhenIsNoUserWithIdLikeFromDto()
    {
        // arrange
        var resetPasswordKey = ResetPasswordKey.Create(Guid.NewGuid());
        var newPassword = "password";

        var now = new DateTime(2022, 01, 01);
        _timeProvider.Current().Returns(now);

        var dto = new ResetPasswordDto
        {
            ResetPasswordKey = resetPasswordKey,
            NewPassword = newPassword,
            ConfirmNewPassword = newPassword
        };
        
        _userRepository.GetAsync(ResetPasswordKey.GetUserId(resetPasswordKey).ToGuid(), CancellationToken.None).ReturnsNull();
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.ResetForgottenPasswordAsync(dto, CancellationToken.None));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotFoundException>();
    }
    
    [Fact]
    public async Task ResetForgottenPasswordAsync_ShouldThrowPasswordAlreadyResetException_WhenPasswordAlreadyReset()
    {
        // arrange
        var user = GetUser();
        var resetPasswordKey = ResetPasswordKey.Create(user.Id);
        var newPassword = "password";

        var now = new DateTime(2022, 01, 01);
        _timeProvider.Current().Returns(now);

        user.ResetPassword = null;

        var dto = new ResetPasswordDto
        {
            ResetPasswordKey = resetPasswordKey,
            NewPassword = newPassword,
            ConfirmNewPassword = newPassword
        };
        
        _userRepository.GetAsync(user.Id, CancellationToken.None).Returns(user);
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.ResetForgottenPasswordAsync(dto, CancellationToken.None));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PasswordAlreadyResetException>();
        exception.ShouldNotBeOfType<UserNotFoundException>();
        exception.ShouldNotBeOfType<ResetPasswordKeyExpiredException>();
    }
    
    #endregion
            
    #region RefreshTokenAsync
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task RefreshTokenAsync_ShouldThrowInvalidRefreshTokenException_WhenRefreshTokenHasInvalidFormat(string? inputRefreshToken)
    {
        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.RefreshTokenAsync(inputRefreshToken, CancellationToken.None));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidRefreshTokenException>();
    }

    [Fact]
    public async Task RefreshTokenAsync_ShouldThrowCannotFindUserByRefreshTokenException_WhenUserWithRefreshTokenNotExists()
    {
        // arrange
        var inputRefreshToken = Guid.NewGuid().ToString();
        _userRepository.GetByRefreshTokenAsync(inputRefreshToken, CancellationToken.None).ReturnsNull();
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.RefreshTokenAsync(inputRefreshToken, CancellationToken.None));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CannotFindUserByRefreshTokenException>();
    }
    
    [Fact]
    public async Task RefreshTokenAsync_ShouldThrowRefreshTokenExpiredException_WhenRefreshTokenExpired()
    {
        // arrange
        const string inputRefreshToken = "FA952ACE-9518-46AD-9554-9789955A1C64";
        const string storedRefreshToken = "D551FFC4-076E-4CF1-9EFD-2B21F4F04A33";
        
        var newRefreshTokenExpiredAt = new DateTime(2024, 04, 01).AddMinutes(1000);

        var user = GetUser();
        user.RefreshToken = new RefreshToken
        {
            Token = storedRefreshToken,
            ExpiredAt = newRefreshTokenExpiredAt
        };
        
        _timeProvider.Current().Returns(newRefreshTokenExpiredAt);
        _userRepository.GetByRefreshTokenAsync(inputRefreshToken, CancellationToken.None).Returns(user);
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.RefreshTokenAsync(inputRefreshToken, CancellationToken.None));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<RefreshTokenExpiredException>();
    }
    
    [Fact]
    public async Task RefreshTokenAsync_ShouldNotThrowRefreshTokenExpiredException_WhenRefreshTokenNotExpired()
    {
        // arrange
        const int refreshTokenExpiredAfterMinutes = 1000;
        const string inputRefreshToken = "FA952ACE-9518-46AD-9554-9789955A1C64";
        const string storedRefreshToken = "D551FFC4-076E-4CF1-9EFD-2B21F4F04A33";
        
        var newRefreshTokenExpiredAt = new DateTime(2024, 04, 01).AddMinutes(refreshTokenExpiredAfterMinutes);

        var user = GetUser();
        user.RefreshToken = new RefreshToken
        {
            Token = storedRefreshToken,
            ExpiredAt = newRefreshTokenExpiredAt
        };
        
        _timeProvider.Current().Returns(newRefreshTokenExpiredAt.AddMinutes(-1));
        _userRepository.GetByRefreshTokenAsync(inputRefreshToken, CancellationToken.None).Returns(user);
        _authenticator.CreateRefreshToken().Returns(new RefreshTokenDto(Guid.NewGuid().ToString(), DateTime.Now.AddMinutes(refreshTokenExpiredAfterMinutes)));
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.RefreshTokenAsync(inputRefreshToken, CancellationToken.None));
        
        // assert
        exception.ShouldBeNull();
    }
    
    [Fact]
    public async Task RefreshTokenAsync_ShouldUpdateUserStoredRefreshToken_WhenRefreshTokenIsValid()
    {
        // arrange
        const int refreshTokenExpiredAfterMinutes = 1000;
        const string inputRefreshToken = "FA952ACE-9518-46AD-9554-9789955A1C64";
        const string storedRefreshToken = "D551FFC4-076E-4CF1-9EFD-2B21F4F04A33";
        
        var newRefreshTokenExpiredAt = new DateTime(2024, 04, 01).AddMinutes(refreshTokenExpiredAfterMinutes);

        var user = GetUser();
        user.RefreshToken = new RefreshToken
        {
            Token = storedRefreshToken,
            ExpiredAt = newRefreshTokenExpiredAt
        };
        
        _timeProvider.Current().Returns(newRefreshTokenExpiredAt.AddMinutes(-1));
        _userRepository.GetByRefreshTokenAsync(inputRefreshToken, CancellationToken.None).Returns(user);
        _authenticator.CreateRefreshToken().Returns(new RefreshTokenDto(Guid.NewGuid().ToString(), DateTime.Now.AddMinutes(refreshTokenExpiredAfterMinutes)));
        
        // act
        await _accountService.RefreshTokenAsync(inputRefreshToken, CancellationToken.None);
        
        // assert
        await _userRepository.Received(1).UpdateAsync(Arg.Any<User>(), CancellationToken.None);
    }
    
    [Fact]
    public async Task RefreshTokenAsync_ShouldReturnsNewRefreshTokenAndAccessToken_WhenRefreshTokenIsValid()
    {
        // arrange
        const int refreshTokenExpiredAfterMinutes = 1000;
        const string inputRefreshToken = "FA952ACE-9518-46AD-9554-9789955A1C64";
        const string storedRefreshToken = "D551FFC4-076E-4CF1-9EFD-2B21F4F04A33";
        
        var newRefreshTokenExpiredAt = new DateTime(2024, 04, 01).AddMinutes(refreshTokenExpiredAfterMinutes);

        var user = GetUser();
        user.RefreshToken = new RefreshToken
        {
            Token = storedRefreshToken,
            ExpiredAt = newRefreshTokenExpiredAt
        };
        
        _timeProvider.Current().Returns(newRefreshTokenExpiredAt.AddMinutes(-1));
        _userRepository.GetByRefreshTokenAsync(inputRefreshToken, CancellationToken.None).Returns(user);
        _authenticator.CreateRefreshToken().Returns(new RefreshTokenDto(Guid.NewGuid().ToString(), DateTime.Now.AddMinutes(refreshTokenExpiredAfterMinutes)));
        _authenticator.CreateAccessToken(
            Arg.Any<Guid>(), 
            Arg.Any<Email>(), 
            Arg.Any<Shared.Abstractions.DDD.ValueObjects.Role>(), 
            Arg.Any<Shared.Abstractions.DDD.ValueObjects.Subscription>())
            .Returns(new AccessTokenDto());
        
        // act
        var result = await _accountService.RefreshTokenAsync(inputRefreshToken, CancellationToken.None);
        
        // assert
        result.ShouldNotBeNull();
        result.ShouldBeOfType<AuthenticationResponse>();
        result.RefreshToken.ShouldBeOfType<RefreshTokenDto>().ShouldNotBeNull();
        result.AccessToken.ShouldBeOfType<AccessTokenDto>().ShouldNotBeNull();
    }
    
    #endregion
                
    #region RevokeRefreshTokenAsync

    [Fact]
    public async Task RevokeRefreshTokenAsync_ShouldThrowUserNotFoundException_WhenUserNotExist()
    {
        // arrange
        _userRepository.GetAsync(Arg.Any<Guid>(), CancellationToken.None).ReturnsNull();
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _accountService.RevokeRefreshTokenAsync(Arg.Any<Guid>(), CancellationToken.None));
        
        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotFoundException>();
    }

    [Fact]
    public async Task RevokeRefreshTokenAsync_ShouldSetNullForRefreshToken_WhenUserExists()
    {
        // arrange
        var user = GetUser();
        user.RefreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            ExpiredAt = DateTime.Now
        };
        _userRepository.GetAsync(user.Id, CancellationToken.None).Returns(user);
        
        // act
        await _accountService.RevokeRefreshTokenAsync(user.Id, CancellationToken.None);
        
        // assert
        user.ShouldNotBeNull();
        user.RefreshToken.ShouldBeNull();
    }

    #endregion
    
    #region Arrange
    private readonly IAccountService _accountService;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IMessageBroker _messageBroker;
    private readonly IAuthenticator _authenticator;
    private readonly ITimeProvider _timeProvider;
    private readonly IRequestContext _requestContext;
    private readonly IPasswordValidator _passwordValidator;

    public AccountServiceTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordManager = Substitute.For<IPasswordManager>();
        _messageBroker = Substitute.For<IMessageBroker>();
        _authenticator = Substitute.For<IAuthenticator>();
        _timeProvider = Substitute.For<ITimeProvider>(); 
        _requestContext = Substitute.For<IRequestContext>();
        _passwordValidator = Substitute.For<IPasswordValidator>();
        
        _accountService = new AccountService(
            _userRepository,
            _authenticator,
            _passwordManager,
            _timeProvider,
            _messageBroker,
            _requestContext,
            new PasswordResetOptions
            {
                ExpirationMinutes = 10,
                RedirectTo = "http://redirect-to.com",
                UseResetPasswordPolicyPolicy = true,
                ResetPasswordPolicyMultiplierMinutes = 5
            },
            _passwordValidator,
            new AuthOptions
            {
                UseRefreshToken = true,
                RefreshTokenExpiryMinutes = 1000
            });
    }
    
    private static User GetUser() => new()
    {
        Id = "36FF203C-DD33-492C-9690-35698FE02188".ToGuid(),
        FullName = "Name",
        Email = "email@email.com",
        Password = "secret-password",
        CreatedAt = new DateTime(2023, 7, 13),
        IsActive = true,
        Role = new Role
        {
            Value = SystemRole.None,
            GrantedAt = DateTime.Now,
            GrantedBy = Guid.NewGuid()
        },
        Subscription = new Subscription
        {
            Value = SystemSubscription.StandardInvestor,
            GrantedAt = DateTime.Now,
            GrantedBy = Guid.NewGuid(),
            ExpiredAt = null,
            ChangeSource = SubscriptionChangeSource.NeverChanged
        }
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