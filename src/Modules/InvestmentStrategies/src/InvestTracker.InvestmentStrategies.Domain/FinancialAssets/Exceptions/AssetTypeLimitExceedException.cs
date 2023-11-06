using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Exceptions;

internal sealed class AssetTypeLimitExceedException : InvestTrackerException
{
    public AssetTypeLimitExceedException(string subscription) 
        : base($"Asset types limit for subscription: '{subscription}' exceeded.")
    {
    }
}