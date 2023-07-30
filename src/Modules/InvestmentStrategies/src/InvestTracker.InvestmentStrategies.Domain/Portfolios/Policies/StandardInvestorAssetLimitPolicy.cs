using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Assets;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies;

internal class StandardInvestorAssetLimitPolicy : IAssetLimitPolicy
{
    private const int StandardInvestorAssetLimit = 3;

    public bool CanBeApplied(Subscription subscription) 
        => subscription == SystemSubscription.StandardInvestor;

    public bool CanAddAsset(ISet<Asset> assets) 
        => assets.Count >= StandardInvestorAssetLimit;
}