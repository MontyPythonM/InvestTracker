using InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.AssetTypeLimitPolicy;

internal class ProfessionalInvestorFinancialAssetLimitPolicy : IFinancialAssetLimitPolicy
{
    private const int ProfessionalInvestorAssetLimit = 10;

    public bool CanBeApplied(Subscription subscription) 
        => subscription == SystemSubscription.ProfessionalInvestor;

    public bool CanAddAsset(IFinancialAsset asset, IEnumerable<IFinancialAsset> existingAssets)
    {
        var assets = existingAssets.ToList();
        
        return !IsAssetTypesNumberExceed(assets) && !IsTryAddAnotherConcreteCurrencyCash(asset, assets);    }
    
    private static bool IsAssetTypesNumberExceed(IEnumerable<IFinancialAsset> existingAssets)
    {
        var existingAssetTypes = existingAssets
            .Select(asset => asset.GetType())
            .Distinct();
        
        return existingAssetTypes.Count() < ProfessionalInvestorAssetLimit;
    }

    private static bool IsTryAddAnotherConcreteCurrencyCash(IFinancialAsset asset, 
        IEnumerable<IFinancialAsset> existingAssets)
    {
        var existingCashCurrencies = existingAssets.Select(a => a.Currency);

        return asset.GetType() == typeof(Cash) && existingCashCurrencies.Contains(asset.Currency);
    }
}