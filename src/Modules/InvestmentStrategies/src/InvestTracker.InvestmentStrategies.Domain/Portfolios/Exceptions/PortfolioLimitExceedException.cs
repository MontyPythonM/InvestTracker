using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal class PortfolioLimitExceedException : InvestTrackerException
{
    public PortfolioLimitExceedException(string subscription) 
        : base($"Portfolios limit for subscription: '{subscription}' exceeded.")
    {
    }
}