using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Policies.AssetLimitPolicy;

internal class AdvisorAssetLimitPolicy : IAssetLimitPolicy
{
    private const int AdvisorAssetLimit = 30;

    public bool CanBeApplied(Subscription subscription) 
        => subscription == SystemSubscription.Advisor;

    public bool CanAddAsset(ISet<AssetId> assets) 
        => assets.Count < AdvisorAssetLimit;
}