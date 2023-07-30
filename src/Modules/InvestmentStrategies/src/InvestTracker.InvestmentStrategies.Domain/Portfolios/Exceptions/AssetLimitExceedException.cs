using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal class AssetLimitExceedException : InvestTrackerException
{
    public AssetLimitExceedException(string subscription) 
        : base($"Assets limit for subscription: '{subscription}' exceeded.")
    {
    }
}