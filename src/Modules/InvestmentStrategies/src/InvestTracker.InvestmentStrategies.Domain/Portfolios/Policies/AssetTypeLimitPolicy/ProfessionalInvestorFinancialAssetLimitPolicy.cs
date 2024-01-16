using InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.AssetTypeLimitPolicy;

internal class ProfessionalInvestorFinancialAssetLimitPolicy : IFinancialAssetLimitPolicy
{
    private const int ProfessionalInvestorAssetLimit = 10;

    public bool CanBeApplied(Subscription subscription) 
        => subscription == SystemSubscription.ProfessionalInvestor;

    public bool CanAddAsset(IFinancialAsset newAsset, List<IFinancialAsset> existingAssets)
    {
        return !existingAssets.IsAssetTypesNumberExceed(ProfessionalInvestorAssetLimit) && 
               !existingAssets.IsConcreteCurrencyCashDuplicated(newAsset);
    }
}