using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;

public interface IInvestmentStrategyRepository
{
    Task<InvestmentStrategy> GetAsync(InvestmentStrategyId id, CancellationToken token);
    Task<InvestmentStrategy> GetAsync(PortfolioId portfolioId, CancellationToken token);
    Task AddAsync(InvestmentStrategy strategy, CancellationToken token);
}