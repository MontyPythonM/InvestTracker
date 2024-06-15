using InvestTracker.Shared.Abstractions.Exceptions;
using InvestTracker.Shared.Abstractions.Types;

namespace InvestTracker.Users.Core.Exceptions;

internal sealed class ResetPasswordRequiredWaitingException : InvestTrackerException
{
    public ResetPasswordRequiredWaitingException(DateTime waitUntil) 
        : base($"Password reset action has already been triggered and you must wait until {waitUntil.ToFormatString()} before performing it again")
    {
    }
}