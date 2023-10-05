using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Users.Core.Enums;

namespace InvestTracker.Users.Core.Entities;

public class Subscription
{
    public string Value { get; set; } = SystemSubscription.None;
    public Guid? GrantedBy { get; set; }
    public DateTime GrantedAt { get; set; }
    public DateTime? ExpiredAt { get; set; }
    public SubscriptionChangeSource ChangeSource { get; set; } = SubscriptionChangeSource.NeverChanged;
}