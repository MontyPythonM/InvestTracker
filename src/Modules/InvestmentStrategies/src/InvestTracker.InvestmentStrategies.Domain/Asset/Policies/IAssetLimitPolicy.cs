using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Policies;

public interface IAssetLimitPolicy
{
    bool CanBeApplied(Subscription subscription);
    bool CanAddNewAsset(Portfolio portfolio);
}