using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Notifications.Core.Persistence.Repositories;

internal sealed class GlobalSettingsRepository : IGlobalSettingsRepository
{
    private readonly NotificationsDbContext _dbContext;
    private readonly ITimeProvider _timeProvider;
    
    public GlobalSettingsRepository(NotificationsDbContext dbContext, ITimeProvider timeProvider)
    {
        _dbContext = dbContext;
        _timeProvider = timeProvider;
    }

    public async Task<GlobalSettings> GetAsync(CancellationToken token = default)
    {
        var now = _timeProvider.Current();
        
        var settings = await _dbContext.GlobalSettings
            .Where(s => s.EffectiveFrom <= now)
            .OrderByDescending(s => s.EffectiveFrom)
            .FirstOrDefaultAsync(token);
        
        if (settings is null)
        {
            settings = GlobalSettings.CreateInitialSetup();
            await CreateAsync(settings, token);
        }

        return settings;
    }

    public async Task CreateAsync(GlobalSettings setup, CancellationToken token = default)
    {
        await _dbContext.GlobalSettings.AddAsync(setup, token);
        await _dbContext.SaveChangesAsync(token);
    }
}