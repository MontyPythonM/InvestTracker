using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class AssetLimitPolicyNotFoundException : InvestTrackerException
{
    public AssetLimitPolicyNotFoundException(string subscription) 
        : base($"No financial asset limit policy found for subscription: {subscription}.")
    {
    }
}