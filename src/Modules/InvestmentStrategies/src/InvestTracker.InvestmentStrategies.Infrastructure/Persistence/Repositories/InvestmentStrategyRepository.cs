using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
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

    public async Task<InvestmentStrategy?> GetAsync(InvestmentStrategyId id, CancellationToken token = default)
    {
        return await _context.InvestmentStrategies.SingleOrDefaultAsync(strategy => strategy.Id == id, token);
    }
    
    public async Task<IEnumerable<InvestmentStrategy>> GetOwnerStrategiesAsync(StakeholderId ownerId, CancellationToken token = default)
    {
        return await _context.InvestmentStrategies
            .Where(strategy => strategy.Owner.Equals(ownerId))
            .ToListAsync(token);
    }

    // TODO nie działa bo ma problem:
    // The property 'InvestmentStrategy.Portfolios' is a collection or enumeration type with a value converter but with no value comparer.
    // Set a value comparer to ensure the collection/enumeration elements are compared correctly.
    public async Task<IEnumerable<PortfolioId>> GetOwnerPortfoliosAsync(StakeholderId ownerId, bool asNoTracking = false, CancellationToken token = default)
    {
        return await _context.InvestmentStrategies
            .AsNoTracking()
            .Where(strategy => strategy.Owner.Equals(ownerId))
            .SelectMany(strategy => strategy.Portfolios)
            .ToListAsync(token);
    }
    
    public async Task<InvestmentStrategy?> GetByPortfolioAsync(PortfolioId portfolioId, CancellationToken token = default)
    {
        return await _context.InvestmentStrategies
            .SingleOrDefaultAsync(strategy => strategy.Portfolios.Contains(portfolioId), token);
    }

    public async Task<IEnumerable<InvestmentStrategy>> GetByCollaborationAsync(StakeholderId advisorId, 
        StakeholderId principalId, CancellationToken token = default)
    {
        return await _context.InvestmentStrategies
            .Where(strategy => strategy.Owner == principalId && strategy.Collaborators.Contains(advisorId))
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

    public async Task UpdateRangeAsync(IEnumerable<InvestmentStrategy> strategies, CancellationToken token = default)
    {
        _context.InvestmentStrategies.UpdateRange(strategies);
        await _context.SaveChangesAsync(token);
    }
}