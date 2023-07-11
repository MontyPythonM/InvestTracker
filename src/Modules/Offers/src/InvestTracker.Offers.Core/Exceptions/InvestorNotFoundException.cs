using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Offers.Core.Exceptions;

internal class InvestorNotFoundException : InvestTrackerException
{
    public InvestorNotFoundException(Guid id) : base($"Investor with ID: {id} not found.")
    {
    }
}