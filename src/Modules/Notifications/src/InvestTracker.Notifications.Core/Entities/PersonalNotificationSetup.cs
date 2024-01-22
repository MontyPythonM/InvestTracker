using InvestTracker.Notifications.Core.Entities.Base;

namespace InvestTracker.Notifications.Core.Entities;

public class PersonalNotificationSetup : NotificationSetup
{
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}