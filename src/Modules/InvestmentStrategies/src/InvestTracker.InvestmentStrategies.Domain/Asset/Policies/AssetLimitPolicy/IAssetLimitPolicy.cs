using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Policies.AssetLimitPolicy;

public interface IAssetLimitPolicy
{
    bool CanBeApplied(Subscription subscription);
    bool CanAddAsset(ISet<AssetId> assets);
}