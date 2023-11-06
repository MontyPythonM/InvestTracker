using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Repositories;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;

internal sealed class FinancialAssetRepository : IFinancialAssetRepository
{
    private readonly InvestmentStrategiesDbContext _context;

    public FinancialAssetRepository(InvestmentStrategiesDbContext context)
    {
        _context = context;
    }
    
    public async Task<T?> GetAsync<T>(FinancialAssetId id, CancellationToken token = default) 
        where T : FinancialAsset
    {
        return await _context.FinancialAssets
            .OfType<T>()
            .SingleOrDefaultAsync(asset => asset.Id == id, token);
    }

    public async Task<IEnumerable<FinancialAsset>> GetAssetsAsync(PortfolioId portfolioId, CancellationToken token = default)
    {
        return await _context.FinancialAssets
            .Where(asset => asset.PortfolioId == portfolioId)
            .ToListAsync(token);
    }

    public async Task CreateAsync(FinancialAsset asset, CancellationToken token = default)
    {
        await _context.FinancialAssets.AddAsync(asset, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(FinancialAsset asset, CancellationToken token = default)
    {
        _context.FinancialAssets.Update(asset);
        await _context.SaveChangesAsync(token);
    }
}