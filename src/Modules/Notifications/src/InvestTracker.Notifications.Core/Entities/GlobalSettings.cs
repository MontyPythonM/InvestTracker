using InvestTracker.Notifications.Core.Entities.Base;

namespace InvestTracker.Notifications.Core.Entities;

public class GlobalSettings : NotificationSettings
{
    public DateTime EffectiveFrom { get; set; }

    public static GlobalSettings CreateInitialSetup()
    {
        return new GlobalSettings
        {
            Id = Guid.NewGuid(),
            EffectiveFrom = DateTime.MinValue,
        };
    }
}