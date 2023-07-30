using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;

internal class StrategyLimitExceededException : InvestTrackerException
{
    public StrategyLimitExceededException(string subscription) 
        : base($"Investment strategies limit for subscription: '{subscription}' exceeded.")
    {
    }
}