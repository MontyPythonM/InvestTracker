namespace InvestTracker.Notifications.Api.Dto;

public class SendNotificationDto
{
    public string Message { get; set; } = string.Empty;
    public IEnumerable<Guid> RecipientIds { get; set; } = new List<Guid>();
}