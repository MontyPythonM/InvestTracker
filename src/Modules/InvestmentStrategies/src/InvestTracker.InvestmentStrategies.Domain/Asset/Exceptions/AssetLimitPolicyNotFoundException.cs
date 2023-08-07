using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;

internal class AssetLimitPolicyNotFoundException : InvestTrackerException
{
    public AssetLimitPolicyNotFoundException(string subscription) 
        : base($"No asset limit policy found for subscription: {subscription}.")
    {
    }
}