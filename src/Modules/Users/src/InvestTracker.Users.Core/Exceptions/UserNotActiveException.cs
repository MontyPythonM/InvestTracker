using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal class UserNotActiveException : InvestTrackerException
{
    public UserNotActiveException(Guid id) : base($"User with ID: {id} is not active.")
    {
    }
}