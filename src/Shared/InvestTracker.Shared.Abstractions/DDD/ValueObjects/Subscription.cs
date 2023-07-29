using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.ValueObjects;

public record Subscription
{
    public string Value { get; }

    public Subscription(string value)
    {
        if (!IsValidSubscription(value))
        {
            throw new SubscriptionNotExistException(value);
        }

        Value = value;
    }
    
    public static implicit operator string(Subscription subscription) => subscription.Value;
    public static implicit operator Subscription(string subscription) => new(subscription);
    
    private static bool IsValidSubscription(string value) 
        => SystemSubscription.Subscriptions.Contains(value) || string.IsNullOrEmpty(value);
}