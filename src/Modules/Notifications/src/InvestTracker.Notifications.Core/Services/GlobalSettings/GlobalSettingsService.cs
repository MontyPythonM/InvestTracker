using InvestTracker.Notifications.Core.Dto;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Notifications.Core.Services.GlobalSettings;

public class GlobalSettingsService : IGlobalSettingsService
{
    private readonly IGlobalSettingsRepository _globalSettingsRepository;
    private readonly ITimeProvider _timeProvider;
    
    public GlobalSettingsService(IGlobalSettingsRepository globalSettingsRepository, ITimeProvider timeProvider)
    {
        _globalSettingsRepository = globalSettingsRepository;
        _timeProvider = timeProvider;
    }

    public async Task<GlobalSettingsDto> GetAsync(CancellationToken token)
    {
        var settings = await _globalSettingsRepository.GetAsync(token);

        if (settings is null)
        {
            settings = Entities.GlobalSettings.CreateInitialSetup();
            await _globalSettingsRepository.CreateAsync(settings, token);
        }

        return MapToDto(settings);
    }

    public async Task SetAsync(SetGlobalSettingsDto dto, CancellationToken token)
    {
        var settings = new Entities.GlobalSettings
        {
            Id = Guid.NewGuid(),
            EffectiveFrom = _timeProvider.Current(),
            EnableNotifications = dto.EnableNotifications,
            EnableEmails = dto.EnableEmails,
            AdministratorsActivity = dto.AdministratorsActivity,
            InvestmentStrategiesActivity = dto.InvestmentStrategiesActivity,
            PortfoliosActivity = dto.PortfoliosActivity,
            AssetActivity = dto.AssetActivity,
            ExistingCollaborationsActivity = dto.ExistingCollaborationsActivity,
            NewCollaborationsActivity = dto.NewCollaborationsActivity
        };

        await _globalSettingsRepository.CreateAsync(settings, token);
    }
    
    private static GlobalSettingsDto MapToDto(Entities.GlobalSettings settings) => new()
    {
        EffectiveFrom = settings.EffectiveFrom,
        EnableNotifications = settings.EnableNotifications,
        EnableEmails = settings.EnableEmails,
        AdministratorsActivity = settings.AdministratorsActivity,
        InvestmentStrategiesActivity = settings.InvestmentStrategiesActivity,
        PortfoliosActivity = settings.PortfoliosActivity,
        AssetActivity = settings.AssetActivity,
        ExistingCollaborationsActivity = settings.ExistingCollaborationsActivity,
        NewCollaborationsActivity = settings.NewCollaborationsActivity
    };
}