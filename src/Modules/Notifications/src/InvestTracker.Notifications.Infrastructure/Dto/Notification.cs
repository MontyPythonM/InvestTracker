namespace InvestTracker.Notifications.Infrastructure.Dto;

public sealed record Notification
{
    public string Message { get; }
    public ISet<Guid> Recipients { get; }

    public Notification(string message, IEnumerable<Guid> recipients)
    {
        Message = message;
        Recipients = recipients.ToHashSet();
    }

    public Notification(string message, Guid recipient)
    {
        Message = message;
        Recipients = new HashSet<Guid> { recipient };        
    }
}