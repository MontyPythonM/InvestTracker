using InvestTracker.Users.Core.Dtos;
using InvestTracker.Users.Core.Entities;
using InvestTracker.Users.Core.Interfaces;
using InvestTracker.Users.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Users.Core.Services;

internal sealed class UserService : IUserService
{
    private readonly UsersDbContext _context;

    public UserService(UsersDbContext context)
    {
        _context = context;
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
                Subscription = GetUserSubscription(user.Subscription),
                Role = GetUserRole(user.Role)
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
                Subscription = GetUserSubscription(user.Subscription),
                Role = GetUserRole(user.Role)
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
                    GrantedAt = user.Subscription.GrantedAt,
                    GrantedBy = user.Subscription.GrantedBy
                }
            })
            .SingleOrDefaultAsync(user => user.Id == id, token);
    
    private static string GetUserSubscription(Subscription? subscription) 
        => subscription is null ? string.Empty : subscription.Value!;
    
    private static string GetUserRole(Role? role) 
        => role is null ? string.Empty : role.Value!;
}