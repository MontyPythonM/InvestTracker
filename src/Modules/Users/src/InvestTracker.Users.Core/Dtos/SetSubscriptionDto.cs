namespace InvestTracker.Users.Core.Dtos;

public class SetSubscriptionDto
{
    public string Subscription { get; set; } = string.Empty;
    public DateTime? ExpiredAt { get; set; }
}