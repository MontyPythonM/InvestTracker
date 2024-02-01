using InvestTracker.Notifications.Core.Entities.Base;

namespace InvestTracker.Notifications.Core.Entities;

public class GlobalNotificationSetup : NotificationSetup
{
    public DateTime EffectiveFrom { get; set; }

    public static GlobalNotificationSetup CreateInitialSetup()
    {
        return new GlobalNotificationSetup
        {
            Id = Guid.NewGuid(),
            EffectiveFrom = DateTime.MinValue,
        };
    }
}