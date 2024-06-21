using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal sealed class CannotChangeOwnAccountActivationException : InvestTrackerException
{
    public CannotChangeOwnAccountActivationException() : base("Cannot change own account activation settings")
    {
    }
}