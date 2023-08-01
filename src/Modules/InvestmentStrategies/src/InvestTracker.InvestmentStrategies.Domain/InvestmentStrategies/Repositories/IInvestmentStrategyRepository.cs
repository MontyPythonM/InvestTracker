using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;

public interface IInvestmentStrategyRepository
{
    Task<IEnumerable<InvestmentStrategy>> BrowseAsync(StakeholderId ownerId, CancellationToken token = default);
    Task<InvestmentStrategy?> GetAsync(InvestmentStrategyId id, CancellationToken token = default);
    Task<InvestmentStrategy?> GetAsync(PortfolioId portfolioId, CancellationToken token = default);
    Task AddAsync(InvestmentStrategy strategy, CancellationToken token = default);
    Task UpdateAsync(InvestmentStrategy strategy, CancellationToken token = default);
}