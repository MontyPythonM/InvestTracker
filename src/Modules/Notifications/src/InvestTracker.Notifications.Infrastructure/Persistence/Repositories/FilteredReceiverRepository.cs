using System.Linq.Expressions;
using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Notifications.Core.Entities.Base;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Infrastructure.Interfaces;
using InvestTracker.Shared.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Notifications.Infrastructure.Persistence.Repositories;

internal sealed class FilteredReceiverRepository : IFilteredReceiverRepository
{
    private readonly NotificationsDbContext _dbContext;
    private readonly IGlobalSettingsRepository _globalSettingsRepository;
    
    public FilteredReceiverRepository(NotificationsDbContext dbContext, IGlobalSettingsRepository globalSettingsRepository)
    {
        _dbContext = dbContext;
        _globalSettingsRepository = globalSettingsRepository;
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
}