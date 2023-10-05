using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.StrategyLimitPolicy;

internal class NoneStrategyLimitPolicy : IStrategyLimitPolicy
{
    public bool CanBeApplied(Subscription subscription) 
        => subscription == SystemSubscription.None;

    public bool CanAddInvestmentStrategy(StakeholderId ownerId, IEnumerable<InvestmentStrategy> strategies)
        => false;
}