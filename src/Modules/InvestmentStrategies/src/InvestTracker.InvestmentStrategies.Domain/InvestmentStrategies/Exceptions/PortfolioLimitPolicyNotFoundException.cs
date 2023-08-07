using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;

internal class PortfolioLimitPolicyNotFoundException : InvestTrackerException
{
    public PortfolioLimitPolicyNotFoundException(string subscription) 
        : base($"No portfolio limit policy found for subscription: {subscription}.")
    {
    }
}