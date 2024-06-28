using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal class SubscriptionNotFoundException : InvestTrackerException
{
    public SubscriptionNotFoundException(string value) : base($"Subscription {value} not found")
    {
    } 
}