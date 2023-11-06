using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Policies.AssetLimitPolicy;

public interface IAssetTypeLimitPolicy
{
    bool CanBeApplied(Subscription subscription);
    bool CanAddAssetType(ISet<FinancialAsset> assets);
}