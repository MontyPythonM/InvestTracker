using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Policies.AssetLimitPolicy;

internal class ProfessionalInvestorAssetLimitPolicy : IAssetLimitPolicy
{
    private const int ProfessionalInvestorAssetLimit = 10;

    public bool CanBeApplied(Subscription subscription) 
        => subscription == SystemSubscription.ProfessionalInvestor;

    public bool CanAddAsset(ISet<AssetId> assets) 
        => assets.Count < ProfessionalInvestorAssetLimit;
}