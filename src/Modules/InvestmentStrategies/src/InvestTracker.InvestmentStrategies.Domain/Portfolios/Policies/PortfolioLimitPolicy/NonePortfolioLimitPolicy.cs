using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.PortfolioLimitPolicy;

internal class NonePortfolioLimitPolicy : IPortfolioLimitPolicy
{
    public bool CanBeApplied(Subscription subscription)
        => subscription == SystemSubscription.None;

    public bool CanAddPortfolio(ISet<Portfolio> portfolios)
        => false;
}