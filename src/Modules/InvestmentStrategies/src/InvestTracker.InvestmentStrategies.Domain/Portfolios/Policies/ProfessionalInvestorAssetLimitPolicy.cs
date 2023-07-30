using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Assets;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies;

internal class ProfessionalInvestorAssetLimitPolicy : IAssetLimitPolicy
{
    private const int ProfessionalInvestorAssetLimit = 10;

    public bool CanBeApplied(Subscription subscription) 
        => subscription == SystemSubscription.ProfessionalInvestor;

    public bool CanAddAsset(ISet<Asset> assets) 
        => assets.Count >= ProfessionalInvestorAssetLimit;
}