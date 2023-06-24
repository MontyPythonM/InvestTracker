using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Offers.Core.Exceptions;

internal class AdvisorNotFoundException : InvestTrackerException
{
    public AdvisorNotFoundException(Guid advisorId) : base($"Advisor with ID: {advisorId} not found.")
    {
    } 
}