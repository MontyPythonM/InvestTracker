using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class AssetLimitExceedException : InvestTrackerException
{
    public AssetLimitExceedException(string subscription) 
        : base($"Financial assets limit for subscription: '{subscription}' exceeded.")
    {
    }
}