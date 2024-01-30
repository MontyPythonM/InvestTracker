namespace InvestTracker.Notifications.Core.Dto;

public class Notification
{
    public string Message { get; init; }
    public ISet<Guid> Recipients { get; init; }

    public Notification(string message, ISet<Guid> recipients)
    {
        Message = message;
        Recipients = recipients;
    }
}