using InvestTracker.Notifications.Core.Dto;

namespace InvestTracker.Notifications.Core.Interfaces;

public interface IGlobalSettingsService
{
    Task<GlobalSettingsDto> GetAsync(CancellationToken token);
    Task SetAsync(SetGlobalSettingsDto dto, CancellationToken token);
}