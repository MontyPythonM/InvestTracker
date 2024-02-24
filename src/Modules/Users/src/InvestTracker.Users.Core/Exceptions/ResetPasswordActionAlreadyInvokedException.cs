using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal sealed class ResetPasswordActionAlreadyInvokedException : InvestTrackerException
{
    public ResetPasswordActionAlreadyInvokedException(DateTime expiredAt) 
        : base($"Reset password action was already invoked. Try again after {expiredAt:dd-MM-yyyy hh:mm:ss}")
    {
    }
}