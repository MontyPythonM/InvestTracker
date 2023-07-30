using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Policies;

public class ProfessionalInvestorAssetLimitPolicy : IAssetLimitPolicy
{
    private const int ProfessionalInvestorAssetLimit = 10;
    
    public bool CanBeApplied(Subscription subscription) 
        => subscription == SystemSubscription.ProfessionalInvestor;
    
    public bool CanAddNewAsset(Portfolio portfolio)
        => portfolio.Assets.Count() >= ProfessionalInvestorAssetLimit;
}