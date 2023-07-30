using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.PortfolioLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Services;

public class PortfolioInStrategyService : IPortfolioInStrategyService
{
    private readonly IEnumerable<IPortfolioLimitPolicy> _policies;

    public PortfolioInStrategyService(IEnumerable<IPortfolioLimitPolicy> policies)
    {
        _policies = policies;
    }
    
    public Portfolio CreatePortfolio(InvestmentStrategy investmentStrategy, Subscription subscription,
        PortfolioId portfolioId, Title title, Note? note, Description? description)
    {
        var policy = _policies.SingleOrDefault(policy => policy.CanBeApplied(subscription));

        if (policy is null)
        {
            throw new PortfolioLimitPolicyNotFoundException(subscription);
        }

        if (!policy.CanAddPortfolio(investmentStrategy))
        {
            throw new PortfolioLimitExceedException(subscription);
        }

        var portfolio = new Portfolio(portfolioId, title, note, description);

        investmentStrategy.AddPortfolio(portfolio.Id);
        return portfolio;
    }
}