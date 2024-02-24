using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal class UserNotFoundException : InvestTrackerException
{
    public UserNotFoundException(Guid id) : base($"User with ID: {id} not found.")
    {
    }

    public UserNotFoundException(string email) : base($"User with email: {email} not found.")
    {
    }
    
    public UserNotFoundException() : base($"User not found.")
    {
    }
}