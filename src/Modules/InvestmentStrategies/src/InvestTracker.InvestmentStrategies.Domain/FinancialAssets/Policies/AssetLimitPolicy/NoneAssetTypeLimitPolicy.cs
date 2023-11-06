using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Policies.AssetLimitPolicy;

internal class NoneAssetTypeLimitPolicy : IAssetTypeLimitPolicy
{
    public bool CanBeApplied(Subscription subscription)
        => subscription == SystemSubscription.None;

    public bool CanAddAssetType(ISet<FinancialAsset> assets) 
        => false;
}