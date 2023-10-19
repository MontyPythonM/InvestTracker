using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;

public interface IInvestmentStrategyRepository
{
    Task<IEnumerable<InvestmentStrategy>> GetOwnerStrategies(StakeholderId ownerId, CancellationToken token = default);
    Task<InvestmentStrategy?> GetAsync(InvestmentStrategyId id, CancellationToken token = default);
    Task<InvestmentStrategy?> GetByPortfolioAsync(PortfolioId portfolioId, CancellationToken token = default);
    Task<IEnumerable<InvestmentStrategy>> GetByCollaborationAsync(StakeholderId advisorId, StakeholderId principalId, CancellationToken token = default);
    Task<IEnumerable<AssetId>> GetOwnerAssets(StakeholderId owner, CancellationToken token = default);
    Task AddAsync(InvestmentStrategy strategy, CancellationToken token = default);
    Task UpdateAsync(InvestmentStrategy strategy, CancellationToken token = default);
    Task UpdateRangeAsync(IEnumerable<InvestmentStrategy> strategies, CancellationToken token = default);
    Task<bool> HasAsset(StakeholderId owner, AssetId assetId, CancellationToken token = default);
}