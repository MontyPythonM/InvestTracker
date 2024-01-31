namespace InvestTracker.Notifications.Core.Entities.Base;

public abstract class NotificationSetup
{
    public Guid Id { get; set; }
    public bool EnableNotification { get; set; }
    public bool EnableEmail { get; set; }
    public bool EnableAdministrative { get; set; }
}