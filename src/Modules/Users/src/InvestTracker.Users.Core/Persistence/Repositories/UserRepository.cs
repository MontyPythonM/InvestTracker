using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Users.Core.Entities;
using InvestTracker.Users.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Users.Core.Persistence.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly UsersDbContext _context;

    public UserRepository(UsersDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetAsync(Guid id, CancellationToken token)
        => await _context.Users
            .Include(user => user.Role)
            .Include(user => user.Subscription)
            .Include(user => user.ResetPassword)
            .SingleOrDefaultAsync(user => user.Id == id, token);

    public async Task<User?> GetAsync(Email email, CancellationToken token)
        => await _context.Users
            .Include(user => user.Role)
            .Include(user => user.Subscription)
            .Include(user => user.ResetPassword)
            .SingleOrDefaultAsync(user => user.Email == email, token);

    public async Task CreateAsync(User user, CancellationToken token)
    {
        await _context.Users.AddAsync(user, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(User user, CancellationToken token)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(User user, CancellationToken token)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync(token);
    }
}