using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Assets;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies;

public interface IAssetLimitPolicy
{
    bool CanBeApplied(Subscription subscription);
    bool CanAddAsset(ISet<Asset> assets);
}