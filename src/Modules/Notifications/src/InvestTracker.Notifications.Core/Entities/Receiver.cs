using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.Notifications.Core.Entities;

public class Receiver
{
    public Guid Id { get; set; }
    public Email Email { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public Subscription Subscription { get; private set; } = SystemSubscription.None;
    public Role Role { get; private set; } = SystemRole.None;
    
    public NotificationConfiguration NotificationConfiguration { get; set; }
}