using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;

internal class NoAssetLimitPolicyFoundException : InvestTrackerException
{
    public NoAssetLimitPolicyFoundException() : base($"")
    {
    }
}