using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.Notifications.Core.Entities;

public class Receiver
{
    public Guid Id { get; set; }
    public FullName FullName { get; set; }
    public Email Email { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public Subscription Subscription { get; set; }
    public Role Role { get; set; }
    public PersonalNotificationSetup NotificationSetup { get; set; }
}