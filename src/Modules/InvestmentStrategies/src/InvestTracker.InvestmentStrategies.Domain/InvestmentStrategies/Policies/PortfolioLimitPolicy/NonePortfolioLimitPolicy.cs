using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.PortfolioLimitPolicy;

internal class NonePortfolioLimitPolicy : IPortfolioLimitPolicy
{
    public bool CanBeApplied(Subscription subscription)
        => subscription == SystemSubscription.None;

    public bool CanAddPortfolio(ISet<Portfolio> portfolios)
        => false;
}