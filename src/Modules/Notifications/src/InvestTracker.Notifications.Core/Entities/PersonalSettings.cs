using InvestTracker.Notifications.Core.Entities.Base;

namespace InvestTracker.Notifications.Core.Entities;

public class PersonalSettings : NotificationSettings
{
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}