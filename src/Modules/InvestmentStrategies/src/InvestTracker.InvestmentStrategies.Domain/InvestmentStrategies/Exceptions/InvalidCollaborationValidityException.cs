using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;

internal sealed class InvalidCollaborationValidityException : InvestTrackerException
{
    public InvalidCollaborationValidityException(DateTime from, DateTime to) 
        : base($"Invalid collaboration validity for date range from '{from}' to '{to}'.")
    {
    }
}