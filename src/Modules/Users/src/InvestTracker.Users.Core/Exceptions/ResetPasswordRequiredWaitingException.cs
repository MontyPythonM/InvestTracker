using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal sealed class ResetPasswordRequiredWaitingException : InvestTrackerException
{
    public ResetPasswordRequiredWaitingException(DateTime waitUntil) 
        : base($"Password reset action has already been triggered and you must wait until {waitUntil} before performing it again")
    {
    }
}