using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Notifications.Core.Persistence.Repositories;

internal sealed class GlobalNotificationSetupRepository : IGlobalNotificationSetupRepository
{
    private readonly NotificationsDbContext _dbContext;
    private readonly ITimeProvider _timeProvider;
    
    public GlobalNotificationSetupRepository(NotificationsDbContext dbContext, ITimeProvider timeProvider)
    {
        _dbContext = dbContext;
        _timeProvider = timeProvider;
    }

    public async Task<GlobalNotificationSetup?> GetAsync(CancellationToken token = default)
    {
        var now = _timeProvider.Current();
        
        return await _dbContext.GlobalNotificationSetup
            .Where(s => s.EffectiveFrom <= now)
            .OrderBy(s => s.EffectiveFrom)
            .LastOrDefaultAsync(token);
    }

    public async Task CreateAsync(GlobalNotificationSetup setup, CancellationToken token = default)
    {
        await _dbContext.GlobalNotificationSetup.AddAsync(setup, token);
        await _dbContext.SaveChangesAsync(token);
    }
}