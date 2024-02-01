using InvestTracker.Notifications.Core.Enums;

namespace InvestTracker.Notifications.Api.Dto;

public class SendMessageToGroupDto
{
    public string Message { get; set; } = string.Empty;
    public RecipientGroup RecipientGroup { get; set; }
}