using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Repositories;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;

internal sealed class FinancialAssetRepository<T> : IFinancialAssetRepository<T> 
    where T : FinancialAsset
{
    private readonly InvestmentStrategiesDbContext _context;

    public FinancialAssetRepository(InvestmentStrategiesDbContext context)
    {
        _context = context;
    }
    
    public async Task<T?> GetAsync(FinancialAssetId id, CancellationToken token = default)
    {
        return await _context.FinancialAssets
            .OfType<T>()
            .SingleOrDefaultAsync(asset => asset.Id == id, token);
    }

    public async Task CreateAsync(T asset, CancellationToken token = default)
    {
        await _context.FinancialAssets.AddAsync(asset, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(T asset, CancellationToken token = default)
    {
        _context.FinancialAssets.Update(asset);
        await _context.SaveChangesAsync(token);    
    }
}