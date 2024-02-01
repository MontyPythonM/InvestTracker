using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;

internal sealed class InvestmentStrategyLockedException : InvestTrackerException
{
    public InvestmentStrategyLockedException(InvestmentStrategyId strategyId) 
        : base($"Investment strategy with ID: '{strategyId}' is locked and cannot be changed.")
    {
    }
}