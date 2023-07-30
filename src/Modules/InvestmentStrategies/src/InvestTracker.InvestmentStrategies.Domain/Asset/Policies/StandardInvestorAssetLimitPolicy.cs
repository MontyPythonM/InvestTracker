using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Policies;

public class StandardInvestorAssetLimitPolicy : IAssetLimitPolicy
{
    private const int StandardInvestorAssetLimit = 5;
    
    public bool CanBeApplied(Subscription subscription) 
        => subscription == SystemSubscription.StandardInvestor;

    public bool CanAddNewAsset(Portfolio portfolio)
        => portfolio.Assets.Count() >= StandardInvestorAssetLimit;
}