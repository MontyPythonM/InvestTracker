using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal class EmailAlreadyInUseException : InvestTrackerException
{
    public EmailAlreadyInUseException(string email) : base($"Email {email} is already in use.")
    {
    }
}