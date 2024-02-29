namespace InvestTracker.Notifications.Core.Dto.Notifications;

public class DirectNotification
{
    public string Message { get; }
    public ISet<Guid> Recipients { get; }

    public DirectNotification(string message, IEnumerable<Guid> recipients)
    {
        Message = message;
        Recipients = recipients.ToHashSet();
    }

    public DirectNotification(string message, Guid recipient)
    {
        Message = message;
        Recipients = new HashSet<Guid> { recipient };        
    }
}