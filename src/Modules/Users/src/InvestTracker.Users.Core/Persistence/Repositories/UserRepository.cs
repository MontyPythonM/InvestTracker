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
        => await _context.Users.SingleOrDefaultAsync(user => user.Id == id, token);

    public async Task<User?> GetAsync(string email, CancellationToken token)
        => await _context.Users.SingleOrDefaultAsync(user => user.Email.ToLower() == email.ToLower(), token);

    public async Task CreateAsync(User user, CancellationToken token)
    {
        await _context.Users.AddAsync(user, token);
        await _context.SaveChangesAsync(token);
    }
}