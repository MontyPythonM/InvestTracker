using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Users.Core.Entities;

namespace InvestTracker.Users.Core.Interfaces;

public interface IUserRepository
{
    Task<User?> GetAsync(Guid id, CancellationToken token);
    Task<User?> GetAsync(Email email, CancellationToken token);
    Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken token);
    Task CreateAsync(User user, CancellationToken token);
    Task UpdateAsync(User user, CancellationToken token);
    Task DeleteAsync(User user, CancellationToken token);
}