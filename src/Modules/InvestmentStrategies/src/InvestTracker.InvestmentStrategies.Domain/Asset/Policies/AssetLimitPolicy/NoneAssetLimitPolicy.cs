using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Policies.AssetLimitPolicy;

internal class NoneAssetLimitPolicy : IAssetLimitPolicy
{
    public bool CanBeApplied(Subscription subscription)
        => subscription == SystemSubscription.None;

    public bool CanAddAsset(ISet<AssetId> assets)
        => false;
}