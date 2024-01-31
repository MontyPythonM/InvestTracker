using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Notifications.Core.Persistence.Repositories;

internal sealed class ReceiverRepository : IReceiverRepository
{
    private readonly NotificationsDbContext _dbContext;

    public ReceiverRepository(NotificationsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Receiver?> GetAsync(Guid id, CancellationToken token = default)
    {
        return await _dbContext.Receivers
            .Include(r => r.NotificationSetup)
            .SingleOrDefaultAsync(r => r.Id == id, token);
    }

    public async Task<IEnumerable<Receiver>> GetAsync(Role role, CancellationToken token = default)
    {
        return await _dbContext.Receivers
            .Where(r => r.Role == role)
            .ToListAsync(token);
    }

    public async Task<IEnumerable<Receiver>> GetAsync(Subscription subscription, CancellationToken token = default)
    {
        return await _dbContext.Receivers
            .Where(r => r.Subscription == subscription)
            .ToListAsync(token);    
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken token = default)
    {
        return await _dbContext.Receivers.AnyAsync(r => r.Id == id, token);
    }

    public async Task CreateAsync(Receiver receiver, CancellationToken token = default)
    {
        await _dbContext.Receivers.AddAsync(receiver, token);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(Receiver receiver, CancellationToken token = default)
    {
        _dbContext.Receivers.Update(receiver);
        await _dbContext.SaveChangesAsync(token);
    }
}