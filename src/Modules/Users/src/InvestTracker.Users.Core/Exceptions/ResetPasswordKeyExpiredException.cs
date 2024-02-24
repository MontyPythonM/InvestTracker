using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal sealed class ResetPasswordKeyExpiredException : InvestTrackerException
{
    public ResetPasswordKeyExpiredException() : base("Password reset key has expired")
    {
    }
}