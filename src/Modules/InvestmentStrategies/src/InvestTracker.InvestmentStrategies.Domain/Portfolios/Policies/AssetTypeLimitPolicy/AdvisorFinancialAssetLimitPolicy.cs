using InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.AssetTypeLimitPolicy;

internal class AdvisorFinancialAssetLimitPolicy : IFinancialAssetLimitPolicy
{
    private const int AdvisorAssetLimit = 30;

    public bool CanBeApplied(Subscription subscription) 
        => subscription == SystemSubscription.Advisor;

    public bool CanAddAsset(FinancialAsset newAsset, List<FinancialAsset> existingAssets)
    {
        return !existingAssets.IsAssetTypesNumberExceed(AdvisorAssetLimit) && 
               !existingAssets.IsConcreteCurrencyCashDuplicated(newAsset);
    }
}