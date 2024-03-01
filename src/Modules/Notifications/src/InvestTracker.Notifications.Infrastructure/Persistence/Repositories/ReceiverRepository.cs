using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Notifications.Infrastructure.Interfaces;
using InvestTracker.Shared.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Notifications.Infrastructure.Persistence.Repositories;

internal sealed class ReceiverRepository : IReceiverRepository
{
    private readonly NotificationsDbContext _dbContext;
    private readonly IGlobalSettingsRepository _globalSettingsRepository;

    public ReceiverRepository(NotificationsDbContext dbContext, IGlobalSettingsRepository globalSettingsRepository)
    {
        _dbContext = dbContext;
        _globalSettingsRepository = globalSettingsRepository;
    }

    public async Task<Receiver?> GetAsync(Guid id, bool asNoTracking = false, CancellationToken token = default)
    {
        return await _dbContext.Receivers
            .ApplyAsNoTracking(asNoTracking)
            .Include(r => r.PersonalSettings)
            .SingleOrDefaultAsync(r => r.Id == id, token);
    }

    public async Task<IEnumerable<Receiver>> GetAsync(RecipientGroup recipientGroup, bool asNoTracking = false, CancellationToken token = default)
    {
        return await _dbContext.Receivers
            .ApplyAsNoTracking(asNoTracking)
            .Include(r => r.PersonalSettings)
            .FilterByRecipientGroup(recipientGroup)
            .ToListAsync(token);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken token = default)
    {
        return await _dbContext.Receivers
            .AsNoTracking()
            .AnyAsync(r => r.Id == id, token);
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

    public async Task DeleteAsync(Receiver receiver, CancellationToken token = default)
    {
        _dbContext.Receivers.Remove(receiver);
        await _dbContext.SaveChangesAsync(token);
    }
}