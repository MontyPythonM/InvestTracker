using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Policies.AssetLimitPolicy;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Dto;

public sealed record AssetTypeLimitDto(ISet<FinancialAsset> ExistingPortfolioAssets, Subscription Subscription, 
    IEnumerable<IAssetTypeLimitPolicy> Policies);