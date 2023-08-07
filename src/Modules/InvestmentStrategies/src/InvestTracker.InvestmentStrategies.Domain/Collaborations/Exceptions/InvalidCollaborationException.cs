using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Collaborations.Exceptions;

internal class InvalidCollaborationException : InvestTrackerException
{
    public InvalidCollaborationException(Guid id) 
        : base($"Cannot create collaboration where advisor and principal is the same")
    {
    }
}