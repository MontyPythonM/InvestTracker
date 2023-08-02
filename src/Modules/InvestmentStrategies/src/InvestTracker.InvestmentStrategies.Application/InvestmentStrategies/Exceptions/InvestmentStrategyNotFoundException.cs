using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Exceptions;

internal class InvestmentStrategyNotFoundException : InvestTrackerException
{
    public InvestmentStrategyNotFoundException(Guid id) : base($"Investment strategy with ID: {id} not found.")
    {
    }
}