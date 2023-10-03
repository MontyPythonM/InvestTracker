using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Time;
using InvestTracker.Shared.Infrastructure.Types;
using InvestTracker.Users.Core.Dtos;
using InvestTracker.Users.Core.Entities;
using InvestTracker.Users.Core.Events;
using InvestTracker.Users.Core.Exceptions;
using InvestTracker.Users.Core.Interfaces;
using InvestTracker.Users.Core.Persistence;
using InvestTracker.Users.Core.Services;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using Xunit;

namespace InvestTracker.Users.UnitTests.Core.Services;

public class UserServiceTests
{
    #region SetRoleAsync tests
    [Fact]
    public async Task SetRoleAsync_ShouldThrowRoleNotFoundException_WhenRoleNotExist()
    {
        // arrange
        var dto = GetSetRoleDto(Guid.NewGuid(), "not-existing-role");
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _userService.SetRoleAsync(dto, CancellationToken.None));

        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<RoleNotFoundException>();
    }
    
    [Fact]
    public async Task SetRoleAsync_ShouldThrowUserNotFoundException_WhenUserNotExist()
    {
        // arrange
        var dto = GetSetRoleDto();
        
        _userRepository
            .GetAsync(Arg.Any<Guid>(), CancellationToken.None)
            .ReturnsNull();
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _userService.SetRoleAsync(dto, CancellationToken.None));

        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotFoundException>();
    }
    
    [Fact]
    public async Task SetRoleAsync_ShouldPublishEventAndUpdateEntity_WhenRoleChanged()
    {
        // arrange
        var user = GetUser();
        user.Role = new Role
        {
            Value = SystemRole.BusinessAdministrator
        };
        
        var dto = GetSetRoleDto(user.Id, SystemRole.SystemAdministrator);
        
        _userRepository.GetAsync(user.Id, CancellationToken.None).Returns(user);
        _timeProvider.Current().Returns(DateTime.Now);
        _requestContext.Identity.UserId.Returns(Guid.NewGuid());
        
        // act
        await _userService.SetRoleAsync(dto, CancellationToken.None);
        
        // assert
        await _messageBroker.Received(1)
            .PublishAsync(Arg.Is<UserRoleGranted>(e => 
                e.Id == user.Id &&
                e.Role == user.Role.Value));

        await _userRepository.Received(1)
            .UpdateAsync(user, CancellationToken.None);
    }
    
    [Fact]
    public async Task SetRoleAsync_ShouldSetUserRole_WhenRequestIsValid()
    {
        // arrange
        var user = GetUser();
        var dto = GetSetRoleDto(user.Id);
        
        _userRepository.GetAsync(user.Id, CancellationToken.None).Returns(user);
        _timeProvider.Current().Returns(DateTime.Now);
        _requestContext.Identity.UserId.Returns(Guid.NewGuid());
        
        // act
        await _userService.SetRoleAsync(dto, CancellationToken.None);
        
        // assert
        user.Role.ShouldNotBeNull();
        user.Role.Value.ShouldBe(SystemRole.BusinessAdministrator);
    }
    #endregion
    
    #region RemoveRoleAsync tests
    [Fact]
    public async Task RemoveRoleAsync_ShouldThrowUserNotFoundException_WhenUserNotExist()
    {
        // arrange
        var userId = Guid.NewGuid();
        _userRepository.GetAsync(userId, CancellationToken.None).ReturnsNull();
        
        // act
        var exception = await Record.ExceptionAsync(() =>
            _userService.RemoveRoleAsync(userId, CancellationToken.None));

        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotFoundException>();        
        await _messageBroker.Received(0).PublishAsync(Arg.Any<UserRoleRemoved>());
    }

    [Fact]
    public async Task RemoveRoleAsync_ShouldPublishUserRoleRemovedEvent_WhenRoleRemoved()
    {
        // arrange
        var user = GetUser();
        _userRepository.GetAsync(user.Id, CancellationToken.None).Returns(user);

        // act
        await _userService.RemoveRoleAsync(user.Id, CancellationToken.None);

        // assert
        await _messageBroker.Received(1).PublishAsync(Arg.Is<UserRoleRemoved>(e => e.Id == user.Id));
    }
    
    [Fact]
    public async Task RemoveRoleAsync_ShouldClearUserRole_WhenRoleRemoved()
    {
        // arrange
        var user = GetUser();
        _userRepository.GetAsync(user.Id, CancellationToken.None).Returns(user);

        // act
        await _userService.RemoveRoleAsync(user.Id, CancellationToken.None);

        // assert
        user.Role.ShouldNotBeNull();
        user.Role.Value.ShouldBeNull();
    }
    #endregion

    #region Arrange
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IRequestContext _requestContext;
    private readonly UsersDbContext _usersDbContext;
    private readonly ITimeProvider _timeProvider;

    public UserServiceTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _messageBroker = Substitute.For<IMessageBroker>();
        _requestContext = Substitute.For<IRequestContext>();
        _usersDbContext = new UsersDbContext(new DbContextOptionsBuilder<UsersDbContext>().Options);
        _timeProvider = Substitute.For<ITimeProvider>();
        
        _userService = new UserService(
            _usersDbContext, 
            _userRepository,
            _messageBroker,
            _timeProvider,
            _requestContext);
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
    
    private SetRoleDto GetSetRoleDto(Guid userId = default, string role = SystemRole.BusinessAdministrator) 
        => new()
        {
            UserId = default ? Guid.NewGuid() : userId,
            Role = role
        };
    
    #endregion
}