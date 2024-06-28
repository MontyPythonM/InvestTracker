using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Pagination;
using InvestTracker.Shared.Abstractions.Time;
using InvestTracker.Shared.Infrastructure.Pagination;
using InvestTracker.Users.Core.Dtos;
using InvestTracker.Users.Core.Entities;
using InvestTracker.Users.Core.Enums;
using InvestTracker.Users.Core.Events;
using InvestTracker.Users.Core.Exceptions;
using InvestTracker.Users.Core.Interfaces;
using InvestTracker.Users.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Users.Core.Services;

internal sealed class UserService : IUserService
{
    private readonly UsersDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly ITimeProvider _timeProvider;
    private readonly IRequestContext _requestContext;

    public UserService(UsersDbContext context, IUserRepository userRepository, 
        IMessageBroker messageBroker, ITimeProvider timeProvider, IRequestContext requestContext)
    {
        _context = context;
        _userRepository = userRepository;
        _messageBroker = messageBroker;
        _timeProvider = timeProvider;
        _requestContext = requestContext;
    }

    public async Task<UserDto?> GetUserAsync(Guid id, CancellationToken token)
        => await _context.Users
            .AsNoTracking()
            .Include(user => user.Subscription)
            .Include(user => user.Role)
            .Select(user => new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone ?? string.Empty,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                Subscription = user.Subscription.Value,
                Role = user.Role.Value
            })
            .SingleOrDefaultAsync(user => user.Id == id, token);

    public async Task<Paged<UserDto>> GetUsersAsync(IPagedQuery query, CancellationToken token)
        => await _context.Users
            .AsNoTracking()
            .Include(user => user.Subscription)
            .Include(user => user.Role)
            .Select(user => new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                Subscription = user.Subscription.Value,
                Role = user.Role.Value
            })
            .PaginateAsync(query, token);

    public async Task<UserDetailsDto?> GetUserDetailsAsync(Guid id, CancellationToken token)
        => await _context.Users
            .AsNoTracking()
            .Include(user => user.Subscription)
            .Include(user => user.Role)
            .Select(user => new UserDetailsDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                Subscription = new SubscriptionDto
                {
                  Value = user.Subscription!.Value,
                  GrantedAt = user.Subscription.GrantedAt,
                  GrantedBy = user.Subscription.GrantedBy,
                  ExpiredAt = user.Subscription.ExpiredAt,
                  ChangeSource = user.Subscription.ChangeSource
                },
                Role = new RoleDto
                {
                    Value = user.Role!.Value,
                    GrantedAt = user.Role.GrantedAt,
                    GrantedBy = user.Role.GrantedBy
                }
            })
            .SingleOrDefaultAsync(user => user.Id == id, token);

    public async Task SetRoleAsync(Guid userId, SetRoleDto dto, CancellationToken token)
    {
        if (!SystemRole.Roles.Contains(dto.Role))
        {
            throw new RoleNotFoundException(dto.Role);
        }
        
        var user = await _userRepository.GetAsync(userId, token);
        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }

        var modifiedAt = _timeProvider.Current();
        var modifiedBy = _requestContext.Identity.UserId;
        
        user.Role = new Role
        {
            Value = dto.Role,
            GrantedAt = modifiedAt,
            GrantedBy = modifiedBy
        };
        
        await _userRepository.UpdateAsync(user, token);
        
        if (dto.Role is SystemRole.None)
        {
            await _messageBroker.PublishAsync(new UserRoleRemoved(user.Id, modifiedBy));
        }
        else
        {
            await _messageBroker.PublishAsync(new UserRoleGranted(user.Id, user.Role.Value, modifiedBy));
        }
    }

    public async Task SetSubscriptionAsync(Guid userId, SetSubscriptionDto dto, CancellationToken token)
    {
        if (!SystemSubscription.Subscriptions.Contains(dto.Subscription))
        {
            throw new SubscriptionNotFoundException(dto.Subscription);
        }

        if (dto.ExpiredAt is not null && dto.ExpiredAt.Value < _timeProvider.Current())
        {
            throw new SubscriptionExpiredAtInPastException();
        }

        var user = await _userRepository.GetAsync(userId, token);
        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }

        var modifiedAt = _timeProvider.Current();
        var modifiedBy = _requestContext.Identity.UserId;
        
        user.Subscription = new Subscription
        {
            Value = dto.Subscription,
            ExpiredAt = dto.ExpiredAt,
            GrantedAt = modifiedAt,
            GrantedBy = modifiedBy,
            ChangeSource = SubscriptionChangeSource.FromAdministrator
        };
        
        await _userRepository.UpdateAsync(user, token);
        await _messageBroker.PublishAsync(new UserSubscriptionChanged(user.Id, user.FullName, user.Email, user.Subscription.Value, modifiedBy));
    }

    public async Task SetAccountActivationAsync(Guid userId, bool isActive, CancellationToken token)
    {
        var user = await _userRepository.GetAsync(userId, token);
        if (user is null)
            throw new UserNotFoundException(userId);
        
        if (_requestContext.Identity.UserId == user.Id)
            throw new CannotChangeOwnAccountActivationException();
        
        user.IsActive = isActive;
        await _userRepository.UpdateAsync(user, token);
        
        if (isActive)
        {
            await _messageBroker.PublishAsync(new AccountActivated(user.Id, _requestContext.Identity.UserId));
        }
        else
        {
            await _messageBroker.PublishAsync(new AccountDeactivated(user.Id, _requestContext.Identity.UserId));
        }
    }
}