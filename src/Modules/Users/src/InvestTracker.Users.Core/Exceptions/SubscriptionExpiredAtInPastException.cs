using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal class SubscriptionExpiredAtInPastException : InvestTrackerException
{
    public SubscriptionExpiredAtInPastException() : base("ExpiredAt value cannot be in past")
    {
    }
}