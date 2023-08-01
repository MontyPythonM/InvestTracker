using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;

public interface IInvestmentStrategyRepository
{
    Task<InvestmentStrategy?> GetAsync(InvestmentStrategyId id, CancellationToken token = default);
    Task<InvestmentStrategy?> GetAsync(PortfolioId portfolioId, CancellationToken token = default);
    Task AddAsync(InvestmentStrategy strategy, CancellationToken token = default);
}