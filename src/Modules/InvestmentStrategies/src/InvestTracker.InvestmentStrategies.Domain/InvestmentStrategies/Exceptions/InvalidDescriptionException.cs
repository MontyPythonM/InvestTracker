using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;

internal sealed class InvalidDescriptionException : InvestTrackerException
{
    public InvalidDescriptionException() : base("Description has invalid format.")
    {
    }
}