using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Policies;

public class AdvisorAssetLimitPolicy : IAssetLimitPolicy
{
    private const int AdvisorAssetLimit = 30;
    
    public bool CanBeApplied(Subscription subscription) 
        => subscription == SystemSubscription.Advisor;
    
    public bool CanAddNewAsset(Portfolio portfolio) 
        => portfolio.Assets.Count() >= AdvisorAssetLimit;
}