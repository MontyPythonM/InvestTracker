using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.PortfolioLimitPolicy;

public interface IPortfolioLimitPolicy
{
    bool CanBeApplied(Subscription subscription);
    bool CanAddPortfolio(ISet<Portfolio> portfolios);
}