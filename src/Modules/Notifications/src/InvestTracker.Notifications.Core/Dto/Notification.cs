namespace InvestTracker.Notifications.Core.Dto;

public sealed class Notification
{
    public string Message { get; }
    public ISet<Guid> Recipients { get; }

    public Notification(string message, IEnumerable<Guid> recipients)
    {
        Message = message;
        Recipients = recipients.ToHashSet();
    }
}