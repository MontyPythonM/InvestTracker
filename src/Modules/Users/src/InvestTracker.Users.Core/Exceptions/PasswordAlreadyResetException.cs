using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal sealed class PasswordAlreadyResetException : InvestTrackerException
{
    public PasswordAlreadyResetException() : base("Your password has already been reset")
    {
    }
}