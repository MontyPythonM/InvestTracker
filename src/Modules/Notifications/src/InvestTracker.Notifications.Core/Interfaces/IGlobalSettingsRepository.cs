using InvestTracker.Notifications.Core.Entities;

namespace InvestTracker.Notifications.Core.Interfaces;

public interface IGlobalSettingsRepository
{
    Task<GlobalSettings?> GetAsync(CancellationToken token = default);
    Task CreateAsync(GlobalSettings setup, CancellationToken token = default);
}