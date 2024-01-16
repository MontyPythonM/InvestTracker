﻿using InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies;

internal static class Extensions
{
    internal static bool IsAssetTypesNumberExceed(this IEnumerable<IFinancialAsset> existingAssets, int assetsLimit)
    {
        var existingAssetTypes = existingAssets
            .Select(asset => asset.GetType())
            .Distinct();
        
        return existingAssetTypes.Count() > assetsLimit;
    }

    internal static bool IsConcreteCurrencyCashDuplicated(this IEnumerable<IFinancialAsset> existingAssets, IFinancialAsset newAsset)
    {
        var existingCashCurrencies = existingAssets
            .OfType<Cash>()
            .Select(a => a.Currency);
        
        return newAsset.GetType() == typeof(Cash) && existingCashCurrencies.Contains(newAsset.Currency);
    }
}