using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.StrategyLimitPolicy;

public interface IStrategyLimitPolicy
{
    bool CanBeApplied(Subscription subscription);
    bool CanAddInvestmentStrategy(StakeholderId ownerId, IEnumerable<InvestmentStrategy> strategies);
}