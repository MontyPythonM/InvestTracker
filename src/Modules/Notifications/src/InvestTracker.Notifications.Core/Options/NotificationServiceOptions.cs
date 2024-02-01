namespace InvestTracker.Notifications.Core.Options;

public class NotificationServiceOptions
{
    public bool Enabled { get; set; }
    public string MethodName { get; set; }
    public bool EnableDetailedErrorsSignalR { get; set; }
}