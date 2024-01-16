using InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.AssetTypeLimitPolicy;

internal class StandardInvestorFinancialAssetLimitPolicy : IFinancialAssetLimitPolicy
{
    private const int StandardInvestorAssetLimit = 3;

    public bool CanBeApplied(Subscription subscription) 
        => subscription == SystemSubscription.StandardInvestor;

    public bool CanAddAsset(IFinancialAsset newAsset, List<IFinancialAsset> existingAssets)
    {
        return !existingAssets.IsAssetTypesNumberExceed(StandardInvestorAssetLimit) && 
               !existingAssets.IsConcreteCurrencyCashDuplicated(newAsset);
    }
}