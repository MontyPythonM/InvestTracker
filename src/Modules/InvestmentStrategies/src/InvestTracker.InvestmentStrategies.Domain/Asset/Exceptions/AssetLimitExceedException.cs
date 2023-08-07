using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;

internal class AssetLimitExceedException : InvestTrackerException
{
    public AssetLimitExceedException(string subscription) 
        : base($"Assets limit for subscription: '{subscription}' exceeded.")
    {
    }
}