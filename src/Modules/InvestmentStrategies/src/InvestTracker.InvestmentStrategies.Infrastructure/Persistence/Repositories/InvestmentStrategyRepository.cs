using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;

internal sealed class InvestmentStrategyRepository : IInvestmentStrategyRepository
{
    public Task<IEnumerable<InvestmentStrategy>> BrowseAsync(StakeholderId ownerId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<InvestmentStrategy?> GetAsync(InvestmentStrategyId id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<InvestmentStrategy?> GetByPortfolioAsync(PortfolioId portfolioId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AssetId>> GetOwnerAssets(StakeholderId owner, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(InvestmentStrategy strategy, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(InvestmentStrategy strategy, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasAsset(StakeholderId owner, AssetId assetId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}