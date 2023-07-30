using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Assets;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies;

internal class AdvisorAssetLimitPolicy : IAssetLimitPolicy
{
    private const int AdvisorAssetLimit = 30;

    public bool CanBeApplied(Subscription subscription) 
        => subscription == SystemSubscription.Advisor;

    public bool CanAddAsset(ISet<Asset> assets) 
        => assets.Count >= AdvisorAssetLimit;
}