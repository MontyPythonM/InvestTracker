using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.PortfolioLimitPolicy;

internal class StandardInvestorPortfolioLimitPolicy : IPortfolioLimitPolicy
{    
    private const int StandardInvestorPortfolioLimit = 3;

    public bool CanBeApplied(Subscription subscription)
        => subscription == SystemSubscription.StandardInvestor;

    public bool CanAddPortfolio(ISet<Portfolio> portfolios)
        => portfolios.Count < StandardInvestorPortfolioLimit;
}