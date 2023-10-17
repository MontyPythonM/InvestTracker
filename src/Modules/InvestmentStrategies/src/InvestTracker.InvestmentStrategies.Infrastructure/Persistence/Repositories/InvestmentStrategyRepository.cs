using InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;

internal sealed class InvestmentStrategyRepository : IInvestmentStrategyRepository
{
    private readonly InvestmentStrategiesDbContext _context;

    public InvestmentStrategyRepository(InvestmentStrategiesDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<InvestmentStrategy>> GetOwnerStrategies(StakeholderId ownerId, CancellationToken token = default)
    {
        return await _context.InvestmentStrategies
            .Where(strategy => strategy.Owner == ownerId)
            .ToListAsync(token);
    }

    public async Task<InvestmentStrategy?> GetAsync(InvestmentStrategyId id, CancellationToken token = default)
    {
        return await _context.InvestmentStrategies.SingleOrDefaultAsync(strategy => strategy.Id == id, token);
    }

    public async Task<InvestmentStrategy?> GetByPortfolioAsync(PortfolioId portfolioId, CancellationToken token = default)
    {
        return await _context.InvestmentStrategies
            .SingleOrDefaultAsync(strategy => strategy.Portfolios
                .Select(portfolio => portfolio.Id)
                .Contains(portfolioId), token);
    }

    public async Task<IEnumerable<AssetId>> GetOwnerAssets(StakeholderId owner, CancellationToken token = default)
    {
        return await _context.InvestmentStrategies
            .Where(strategy => strategy.Owner == owner)
            .SelectMany(strategy => strategy.Portfolios)
            .SelectMany(portfolio => portfolio.Assets)
            .ToListAsync(token);
    }

    public async Task AddAsync(InvestmentStrategy strategy, CancellationToken token = default)
    {
        await _context.InvestmentStrategies.AddAsync(strategy, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(InvestmentStrategy strategy, CancellationToken token = default)
    {
        _context.InvestmentStrategies.Update(strategy);
        await _context.SaveChangesAsync(token);
    }

    public async Task<bool> HasAsset(StakeholderId owner, AssetId assetId, CancellationToken token = default)
    {
        return await _context.InvestmentStrategies
            .Where(strategy => strategy.Owner == owner)
            .SelectMany(strategy => strategy.Portfolios)
            .AnyAsync(portfolio => portfolio.Assets.Contains(assetId), token);
    }
}