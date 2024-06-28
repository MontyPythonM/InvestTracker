using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal class UserNotActiveException : InvestTrackerException
{
    public UserNotActiveException() : base($"User is not active")
    {
    }
}