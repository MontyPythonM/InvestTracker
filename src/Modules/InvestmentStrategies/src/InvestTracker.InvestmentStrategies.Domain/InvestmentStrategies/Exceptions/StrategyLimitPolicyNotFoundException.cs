using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;

internal class StrategyLimitPolicyNotFoundException : InvestTrackerException
{
    public StrategyLimitPolicyNotFoundException(string subscription) 
        : base($"No investment strategy limit policy found for subscription: {subscription}.")
    {
    }
}