using InvestTracker.Users.Core.Enums;

namespace InvestTracker.Users.Core.Dtos;

public class SubscriptionDto
{
    public string? Value { get; set; }
    public Guid? GrantedBy { get; set; }
    public DateTime GrantedAt { get; set; }
    public DateTime? ExpiredAt { get; set; }
    public SubscriptionChangeSource ChangeSource { get; set; }
}