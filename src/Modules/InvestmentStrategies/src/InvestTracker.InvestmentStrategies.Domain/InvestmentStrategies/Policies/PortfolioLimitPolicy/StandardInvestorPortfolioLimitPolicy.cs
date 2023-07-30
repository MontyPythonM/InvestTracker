using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.PortfolioLimitPolicy;

internal class StandardInvestorPortfolioLimitPolicy : IPortfolioLimitPolicy
{
    public bool CanBeApplied(Subscription subscription)
    {
        throw new NotImplementedException();
    }

    public bool CanAddPortfolio(InvestmentStrategy investmentStrategy)
    {
        throw new NotImplementedException();
    }
}