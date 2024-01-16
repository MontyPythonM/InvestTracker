using InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.AssetTypeLimitPolicy;

internal class NoneFinancialAssetLimitPolicy : IFinancialAssetLimitPolicy
{
    public bool CanBeApplied(Subscription subscription)
        => subscription == SystemSubscription.None;

    public bool CanAddAsset(IFinancialAsset asset, List<IFinancialAsset> existingAssets)
        => false;
}