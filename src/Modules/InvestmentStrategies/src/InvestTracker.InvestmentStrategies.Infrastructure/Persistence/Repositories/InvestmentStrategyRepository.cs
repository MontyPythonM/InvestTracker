using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Entities;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Infrastructure.EntityFramework;
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
    
    public async Task<IEnumerable<PortfolioId>> GetOwnerPortfoliosAsync(StakeholderId ownerId, bool asNoTracking = false, 
        CancellationToken token = default)
    {
        var strategies = await _context.InvestmentStrategies
            .ApplyAsNoTracking(asNoTracking)
            .Where(strategy => strategy.Owner.Equals(ownerId))
            .ToListAsync(token);

        var portfolios = strategies
            .SelectMany(strategy => strategy.Portfolios)
            .Select(portfolio => new PortfolioId(portfolio.PortfolioId));

        return portfolios;
    }
    
    public async Task<InvestmentStrategy?> GetByPortfolioAsync(PortfolioId portfolioId, bool asNoTracking = false, 
        CancellationToken token = default)
    {
        return await _context.InvestmentStrategies
            .ApplyAsNoTracking(asNoTracking)
            .SingleOrDefaultAsync(strategy => strategy.Portfolios.Select(p => p.PortfolioId).Contains(portfolioId.Value), token);
    }

    public async Task<IEnumerable<InvestmentStrategy>> GetByCollaborationAsync(StakeholderId advisorId, 
        StakeholderId principalId, CancellationToken token = default)
    {
        return await _context.InvestmentStrategies
            .Where(strategy => strategy.Owner == principalId && strategy.Collaborators.Select(c => c.CollaboratorId).Contains(advisorId))
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

    public async Task<bool> HasAccessAsync(InvestmentStrategyId strategyId, StakeholderId stakeholderId, CancellationToken token = default)
    {
        return await _context.InvestmentStrategies
            .AsNoTracking()
            .AnyAsync(strategy => strategy.Id == strategyId && 
                                  (strategy.Owner == stakeholderId || 
                                   strategy.IsShareEnabled && 
                                   strategy.Collaborators.Select(c => c.CollaboratorId).Contains(stakeholderId.Value)), token);
    }
}