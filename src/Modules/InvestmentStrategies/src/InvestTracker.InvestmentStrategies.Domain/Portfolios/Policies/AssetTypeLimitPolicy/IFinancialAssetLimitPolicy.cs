using InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.AssetTypeLimitPolicy;

public interface IFinancialAssetLimitPolicy
{
    bool CanBeApplied(Subscription subscription);
    bool CanAddAsset(IFinancialAsset asset, List<IFinancialAsset> existingAssets);
}