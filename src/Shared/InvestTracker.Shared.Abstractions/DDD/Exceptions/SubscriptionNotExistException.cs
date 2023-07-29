using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.Exceptions;

public sealed class SubscriptionNotExistException : InvestTrackerException
{
    public SubscriptionNotExistException(string subscription) 
        : base($"Subscription '{subscription}' is not defined in SystemSubscriptions collection.")
    {
    }
}