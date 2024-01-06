using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;

public interface IInvestmentStrategyRepository
{
    Task<InvestmentStrategy?> GetAsync(InvestmentStrategyId id, CancellationToken token = default);
    Task<IEnumerable<InvestmentStrategy>> GetOwnerStrategiesAsync(StakeholderId ownerId, CancellationToken token = default);
    Task<IEnumerable<PortfolioId>> GetOwnerPortfoliosAsync(StakeholderId ownerId, bool asNoTracking = false, CancellationToken token = default);
    Task<InvestmentStrategy?> GetByPortfolioAsync(PortfolioId portfolioId, bool asNoTracking = false, CancellationToken token = default);
    Task<IEnumerable<InvestmentStrategy>> GetByCollaborationAsync(StakeholderId advisorId, StakeholderId principalId, CancellationToken token = default);
    Task AddAsync(InvestmentStrategy strategy, CancellationToken token = default);
    Task UpdateAsync(InvestmentStrategy strategy, CancellationToken token = default);
    Task UpdateRangeAsync(IEnumerable<InvestmentStrategy> strategies, CancellationToken token = default);
    Task<bool> HasAccessAsync(InvestmentStrategyId strategyId, StakeholderId stakeholderId, CancellationToken token = default);
}