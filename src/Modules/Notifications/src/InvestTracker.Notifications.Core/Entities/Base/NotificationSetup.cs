namespace InvestTracker.Notifications.Core.Entities.Base;

public abstract class NotificationSetup
{
    public Guid Id { get; set; }
    public bool Push { get; set; }
    public bool Email { get; set; }
    public bool Administrative { get; set; }
}