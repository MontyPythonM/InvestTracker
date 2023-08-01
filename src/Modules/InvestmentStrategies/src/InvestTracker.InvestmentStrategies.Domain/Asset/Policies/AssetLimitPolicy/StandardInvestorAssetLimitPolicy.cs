using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Policies.AssetLimitPolicy;

internal class StandardInvestorAssetLimitPolicy : IAssetLimitPolicy
{
    private const int StandardInvestorAssetLimit = 3;

    public bool CanBeApplied(Subscription subscription) 
        => subscription == SystemSubscription.StandardInvestor;

    public bool CanAddAsset(ISet<AssetId> assets) 
        => assets.Count >= StandardInvestorAssetLimit;
}