using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Offers.Core.Exceptions;

internal sealed class AdvisorAccessException : InvestTrackerException
{
    public AdvisorAccessException(Guid id) : base($"Current user cannot get access to advisor with ID: '{id}'.")
    {
    }
}