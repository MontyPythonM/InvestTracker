using InvestTracker.Notifications.Core.Entities;

namespace InvestTracker.Notifications.Core.Interfaces;

public interface IGlobalNotificationSetupRepository
{
    Task<GlobalNotificationSetup?> GetAsync(CancellationToken token = default);
    Task CreateAsync(GlobalNotificationSetup setup, CancellationToken token = default);
}