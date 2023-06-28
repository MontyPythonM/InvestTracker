using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Offers.Core.Exceptions;

internal class CollaborationNotFoundException : InvestTrackerException
{
    public CollaborationNotFoundException(Guid advisorId, Guid investorId) 
        : base($"Collaboration between advisor [ID: {advisorId}] and investor [ID: {investorId}] not found.")
    {
    }
}