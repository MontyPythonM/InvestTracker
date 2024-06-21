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

    public static Subscription CreateDefaultSubscription(DateTime now)
    {
        return new Subscription
        {
            Value = SystemSubscription.StandardInvestor,
            ExpiredAt = null,
            GrantedAt = now,
            ChangeSource = SubscriptionChangeSource.Expired,
            GrantedBy = null
        };
    }

    public bool IsExpired(DateTime now) => ExpiredAt is not null && ExpiredAt < now;
}