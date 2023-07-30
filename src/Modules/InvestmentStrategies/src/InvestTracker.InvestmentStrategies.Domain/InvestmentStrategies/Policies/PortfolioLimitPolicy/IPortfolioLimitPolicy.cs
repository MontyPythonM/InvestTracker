using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.PortfolioLimitPolicy;

public interface IPortfolioLimitPolicy
{
    bool CanBeApplied(Subscription subscription);
    bool CanAddPortfolio(InvestmentStrategy investmentStrategy);
}