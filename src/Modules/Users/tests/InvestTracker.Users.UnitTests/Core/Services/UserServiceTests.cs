using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Time;
using InvestTracker.Shared.Infrastructure.Types;
using InvestTracker.Users.Core.Dtos;
using InvestTracker.Users.Core.Entities;
using InvestTracker.Users.Core.Exceptions;
using InvestTracker.Users.Core.Interfaces;
using InvestTracker.Users.Core.Persistence;
using InvestTracker.Users.Core.Services;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Shouldly;
using Xunit;

namespace InvestTracker.Users.UnitTests.Core.Services;

public class UserServiceTests
{
    #region SetRoleAsync
    [Fact]
    public async Task SetRoleAsync_ThrowRoleNotFoundException_WhenRoleNotExist()
    {
        // arrange
        var dto = new SetRoleDto
        {
            UserId = Guid.NewGuid(),
            Role = "not-existing-role"
        };
        
        // act
        var exception = await Record.ExceptionAsync(() => 
            _userService.SetRoleAsync(dto, CancellationToken.None));

        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<RoleNotFoundException>();
    }
    
    [Fact]
    public async Task SetRoleAsync_ThrowUserNotFoundException_WhenUserNotExist()
    {
        // arrange
        var dto = new SetRoleDto
        {
            UserId = Guid.NewGuid(),
            Role = SystemRole.SystemAdministrator
        };
        
        // _userRepository.GetAsync returns null
        // act
        var exception = await Record.ExceptionAsync(() => 
            _userService.SetRoleAsync(dto, CancellationToken.None));

        // assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotFoundException>();
    }
    
    [Fact]
    public async Task SetRoleAsync_PublishUserRoleGrantedEvent_WhenValidRoleChanged()
    {
        // arrange
        
        // act
        
        // assert
    }
    
    [Fact]
    public async Task SetRoleAsync_SetUserRole_WhenRoleChanged()
    {
        // arrange
        var dto = new SetRoleDto
        {
            UserId = _user.Id,
            Role = SystemRole.BusinessAdministrator
        };
        
        _userRepository.GetAsync(_user.Id, CancellationToken.None).Returns(_user);
        _time.Current().Returns(DateTime.Now);
        _requestContext.Identity.UserId.Returns(Guid.NewGuid());
        
        // act
        await _userService.SetRoleAsync(dto, CancellationToken.None);
        
        // assert
        _user.Role.ShouldNotBeNull();
        _user.Role.Value.ShouldBe(SystemRole.BusinessAdministrator);
    }
    #endregion
    
    #region RemoveRoleAsync
    [Fact]
    public async Task RemoveRoleAsync_ThrowUserNotFoundException_WhenUserNotExist()
    {
        // arrange
        
        // act
        
        // assert
    }
    
    [Fact]
    public async Task RemoveRoleAsync_PublishUserRoleRemovedEvent_WhenRoleRemoved()
    {
        // arrange
        
        // act
        
        // assert
    }
    
    [Fact]
    public async Task RemoveRoleAsync_ClearUserRole_WhenRoleRemoved()
    {
        // arrange
        
        // act
        
        // assert
    }
    #endregion

    #region Arrange
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IRequestContext _requestContext;
    private readonly UsersDbContext _usersDbContext;
    private readonly ITime _time;

    public UserServiceTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _messageBroker = Substitute.For<IMessageBroker>();
        _requestContext = Substitute.For<IRequestContext>();
        _usersDbContext = new UsersDbContext(new DbContextOptionsBuilder<UsersDbContext>().Options);
        _time = Substitute.For<ITime>();
        
        _userService = new UserService(
            _usersDbContext, 
            _userRepository,
            _messageBroker,
            _time,
            _requestContext);
    }
    
    private static User _user = new()
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