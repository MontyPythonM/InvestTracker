using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;

internal sealed class InvestmentStrategySharedException : InvestTrackerException
{
    public InvestmentStrategySharedException(Guid id) 
        : base($"Investment strategy with ID: {id} cannot be shared, because this options is disabled.")
    {
    }
}