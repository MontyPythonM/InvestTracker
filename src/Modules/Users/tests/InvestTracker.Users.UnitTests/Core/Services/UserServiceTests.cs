﻿using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Time;
using InvestTracker.Shared.Abstractions.Types;
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
        var dto = GetSetRoleDto("not-existing-role");
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _userService.SetRoleAsync(Guid.NewGuid(), dto, CancellationToken.None));

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
            _userService.SetRoleAsync(Guid.NewGuid(), dto, CancellationToken.None));

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
        
        var dto = GetSetRoleDto(SystemRole.SystemAdministrator);
        var currentUserId = Guid.NewGuid();
        var now = DateTime.Now;
        
        _userRepository.GetAsync(user.Id, CancellationToken.None).Returns(user);
        _timeProvider.Current().Returns(now);
        _requestContext.Identity.UserId.Returns(currentUserId);
        
        // act
        await _userService.SetRoleAsync(user.Id, dto, CancellationToken.None);
        
        // assert
        await _messageBroker.Received(1)
            .PublishAsync(Arg.Is<UserRoleGranted>(e => 
                e.Id == user.Id &&
                e.Role == user.Role.Value &&
                e.ModifiedBy == currentUserId));

        await _userRepository.Received(1).UpdateAsync(user, CancellationToken.None);
    }
    
    [Fact]
    public async Task SetRoleAsync_ShouldSetUserRole_WhenRequestIsValid()
    {
        // arrange
        var user = GetUser();
        var dto = GetSetRoleDto();
        
        _userRepository.GetAsync(user.Id, CancellationToken.None).Returns(user);
        _timeProvider.Current().Returns(DateTime.Now);
        _requestContext.Identity.UserId.Returns(Guid.NewGuid());
        
        // act
        await _userService.SetRoleAsync(user.Id, dto, CancellationToken.None);
        
        // assert
        user.Role.ShouldNotBeNull();
        user.Role.Value.ShouldBe(SystemRole.BusinessAdministrator);
    }
    
    [Fact]
    public async Task SetRoleAsync_ShouldPublishUserRoleRemovedEvent_WhenNewRoleIsNone()
    {
        // arrange
        var user = GetUser();
        var dto = GetSetRoleDto(SystemRole.None);

        _userRepository.GetAsync(user.Id, CancellationToken.None).Returns(user);

        // act
        await _userService.SetRoleAsync(user.Id, dto, CancellationToken.None);

        // assert
        await _messageBroker.Received(1).PublishAsync(Arg.Is<UserRoleRemoved>(e => e.Id == user.Id));
        user.Role.ShouldNotBeNull();
        user.Role.Value.ShouldBe(SystemRole.None);
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
    
    private static SetRoleDto GetSetRoleDto(string role = SystemRole.BusinessAdministrator) 
        => new() { Role = role };
    
    #endregion
}