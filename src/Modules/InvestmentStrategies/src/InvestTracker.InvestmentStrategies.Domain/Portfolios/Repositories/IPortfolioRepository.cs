using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;

public interface IPortfolioRepository
{
    Task<Portfolio?> GetAsync(PortfolioId id, CancellationToken token = default);
    Task<IEnumerable<Portfolio>> GetByInvestmentStrategyAsync(InvestmentStrategyId investmentStrategyId, bool asNoTracking = false, CancellationToken token = default);    
    Task<IEnumerable<Portfolio>> GetOwnerPortfoliosAsync(StakeholderId ownerId, bool asNoTracking = false, CancellationToken token = default);
    Task AddAsync(Portfolio portfolio, CancellationToken token = default);
    Task UpdateAsync(Portfolio portfolio, CancellationToken token = default);
}