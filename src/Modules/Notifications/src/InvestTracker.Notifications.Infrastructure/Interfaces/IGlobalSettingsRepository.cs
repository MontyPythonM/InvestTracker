using InvestTracker.Notifications.Core.Entities;

namespace InvestTracker.Notifications.Infrastructure.Interfaces;

internal interface IGlobalSettingsRepository
{
    Task<GlobalSettings> GetAsync(CancellationToken token = default);
    Task CreateAsync(GlobalSettings setup, CancellationToken token = default);
}