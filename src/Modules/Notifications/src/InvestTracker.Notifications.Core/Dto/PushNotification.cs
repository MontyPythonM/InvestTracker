namespace InvestTracker.Notifications.Core.Dto;

public class PushNotification
{
    public Guid Id { get; set; }
    public DateTime SentAt { get; set; }
    public string Message { get; set; }

    public PushNotification(DateTime sentAt, string message)
    {
        Id = Guid.NewGuid();
        SentAt = sentAt;
        Message = message;
    }
}