using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal sealed class ResetPasswordKeyFormatException : InvestTrackerException
{
    public ResetPasswordKeyFormatException() : base("Reset password key has invalid format")
    {
    }
}