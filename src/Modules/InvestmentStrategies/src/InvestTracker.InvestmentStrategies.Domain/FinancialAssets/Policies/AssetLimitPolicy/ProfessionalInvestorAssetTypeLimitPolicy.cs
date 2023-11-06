using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Policies.AssetLimitPolicy;

internal class ProfessionalInvestorAssetTypeLimitPolicy : IAssetTypeLimitPolicy
{
    private const int ProfessionalInvestorAssetLimit = 10;

    public bool CanBeApplied(Subscription subscription) 
        => subscription == SystemSubscription.ProfessionalInvestor;

    public bool CanAddAssetType(ISet<FinancialAsset> assets)
    {
        var assetTypes = assets
            .Select(asset => asset.GetType())
            .Distinct();

        return assetTypes.Count() < ProfessionalInvestorAssetLimit;
    }
}