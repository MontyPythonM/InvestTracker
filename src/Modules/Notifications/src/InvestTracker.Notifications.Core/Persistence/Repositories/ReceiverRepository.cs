using System.Linq.Expressions;
using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Notifications.Core.Entities.Base;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Notifications.Core.Persistence.Repositories;

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

    public async Task<Receiver?> GetFilteredAsync(Guid id, Expression<Func<NotificationSettings, bool>>? filterBy = null, 
        bool asNoTracking = true, CancellationToken token = default)
    {
        var globalSettings = await _globalSettingsRepository.GetAsync(token);
        
        return await _dbContext.Receivers
            .ApplyAsNoTracking(asNoTracking)
            .Include(r => r.PersonalSettings)
            .FilterByPersonalSetting(filterBy)
            .FilterByGlobalSetting(filterBy, globalSettings)
            .SingleOrDefaultAsync(r => r.Id == id, token);
    }

    public async Task<IEnumerable<Receiver>> GetFilteredAsync(IEnumerable<Guid> receiversIds, Expression<Func<NotificationSettings, bool>>? filterBy = null, 
        bool asNoTracking = true, CancellationToken token = default)
    {
        var globalSettings = await _globalSettingsRepository.GetAsync(token);
        
        return await _dbContext.Receivers
            .ApplyAsNoTracking(asNoTracking)
            .Include(r => r.PersonalSettings)
            .Where(r => receiversIds.Contains(r.Id))
            .FilterByPersonalSetting(filterBy)
            .FilterByGlobalSetting(filterBy, globalSettings)
            .ToListAsync(token);
    }

    public async Task<IEnumerable<Receiver>> GetFilteredAsync(RecipientGroup recipientGroup, Expression<Func<NotificationSettings, bool>>? filterBy = null, 
        bool asNoTracking = true, CancellationToken token = default)
    {
        var globalSettings = await _globalSettingsRepository.GetAsync(token);
        
        return await _dbContext.Receivers
            .ApplyAsNoTracking(asNoTracking)
            .Include(r => r.PersonalSettings)
            .FilterByRecipientGroup(recipientGroup)
            .FilterByPersonalSetting(filterBy)
            .FilterByGlobalSetting(filterBy, globalSettings)
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