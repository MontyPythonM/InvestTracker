using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.StrategyLimitPolicy;

internal class StandardInvestorStrategyLimitPolicy : IStrategyLimitPolicy
{
    private const int StandardInvestorLimit = 1;
    
    public bool CanBeApplied(Subscription subscription)
        => subscription == SystemSubscription.StandardInvestor;

    public bool CanAddInvestmentStrategy(StakeholderId ownerId, IEnumerable<InvestmentStrategy> strategies)
        => strategies.Count(x => x.Owner == ownerId) < StandardInvestorLimit;
}