using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.Shared;

public interface IResourceAccessor
{
    Task<bool> CheckAsync(InvestmentStrategyId strategyId, CancellationToken token = default);
    Task<bool> CheckAsync(PortfolioId portfolioId, CancellationToken token = default);
    bool Check(InvestmentStrategy strategy);
    Task<bool> HasAccessAsync(InvestmentStrategyId strategyId, CancellationToken token = default);
    Task<bool> HasAccessAsync(PortfolioId portfolioId, CancellationToken token = default);
    bool HasAccess(InvestmentStrategy strategy);
}