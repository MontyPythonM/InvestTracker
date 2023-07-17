using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Offers.Core.Exceptions;

internal class AdvisorAlreadyExistsException : InvestTrackerException
{
    public AdvisorAlreadyExistsException(Guid id) : base($"Advisor with ID: {id} already exists.")
    {
    }
}