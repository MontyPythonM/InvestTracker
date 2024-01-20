using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal class InvalidPasswordException : InvestTrackerException
{
    public InvalidPasswordException() : base("Invalid password.")
    {
    }
}