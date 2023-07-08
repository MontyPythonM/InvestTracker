using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal class InvalidCredentialsException : InvestTrackerException
{
    public InvalidCredentialsException() : base("Invalid email or password.")
    {
    }
}