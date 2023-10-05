﻿using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Time;
using InvestTracker.Users.Core.Dtos;
using InvestTracker.Users.Core.Entities;
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
                CreatedAt = user.CreatedAt,
                Subscription = user.Subscription.Value,
                Role = user.Role.Value
            })
            .SingleOrDefaultAsync(user => user.Id == id, token);

    public async Task<IEnumerable<UserDto>> GetUsersAsync(CancellationToken token)
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
                CreatedAt = user.CreatedAt,
                Subscription = user.Subscription.Value,
                Role = user.Role.Value
            })
            .ToListAsync(token);

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
                Phone = user.Phone ?? string.Empty,
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

    public async Task SetRoleAsync(SetRoleDto dto, CancellationToken token)
    {
        if (!SystemRole.Roles.Contains(dto.Role))
        {
            throw new RoleNotFoundException(dto.Role);
        }
        
        var user = await _userRepository.GetAsync(dto.UserId, token);
        if (user is null)
        {
            throw new UserNotFoundException(dto.UserId);
        }

        user.Role = new Role
        {
            Value = dto.Role,
            GrantedAt = _timeProvider.Current(),
            GrantedBy = _requestContext.Identity.UserId
        };
        
        await _userRepository.UpdateAsync(user, token);
        await _messageBroker.PublishAsync(new UserRoleGranted(user.Id, user.Role.Value));
    }

    public async Task RemoveRoleAsync(Guid id, CancellationToken token)
    {
        var user = await _userRepository.GetAsync(id, token);
        if (user is null)
        {
            throw new UserNotFoundException(id);
        }
        
        user.Role = new Role
        {
            Value = SystemRole.None,
            GrantedAt = _timeProvider.Current(),
            GrantedBy = _requestContext.Identity.UserId
        };
        
        await _userRepository.UpdateAsync(user, token);
        await _messageBroker.PublishAsync(new UserRoleRemoved(user.Id));
    }
}