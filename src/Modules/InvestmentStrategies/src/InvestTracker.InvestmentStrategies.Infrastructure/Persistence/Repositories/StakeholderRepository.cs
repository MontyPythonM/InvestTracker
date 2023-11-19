using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;

internal sealed class StakeholderRepository : IStakeholderRepository
{
    private readonly InvestmentStrategiesDbContext _context;

    public StakeholderRepository(InvestmentStrategiesDbContext context)
    {
        _context = context;
    }

    public async Task<Stakeholder?> GetAsync(StakeholderId id, CancellationToken token = default)
    {
        return await _context.Stakeholders.SingleOrDefaultAsync(stakeholder => stakeholder.Id == id, token);
    }

    public async Task<bool> ExistsAsync(StakeholderId id, CancellationToken token = default)
    {
        return await _context.Stakeholders.AnyAsync(stakeholder => stakeholder.Id == id, token);
    }

    public async Task<Subscription?> GetSubscriptionAsync(StakeholderId id, CancellationToken token = default)
    {
        return await _context.Stakeholders
            .AsNoTracking()
            .Where(s => s.Id.Equals(id))
            .Select(stakeholder => stakeholder.Subscription)
            .SingleOrDefaultAsync(token);
    }
    
    public async Task<Subscription?> GetOwnerSubscription(PortfolioId portfolioId, CancellationToken token = default)
    {
        var ownerId = await _context.InvestmentStrategies
            .AsNoTracking()
            .Where(strategy => strategy.Portfolios.Any(portfolio => portfolio.PortfolioId.Equals(portfolioId.Value)))
            .Select(strategy => strategy.Owner)
            .SingleOrDefaultAsync(token);
        
        return await _context.Stakeholders
            .AsNoTracking()
            .Where(stakeholder => stakeholder.Id.Equals(ownerId))
            .Select(stakeholder => stakeholder.Subscription)
            .SingleOrDefaultAsync(token);
    }
    
    public async Task AddAsync(Stakeholder stakeholder, CancellationToken token = default)
    {
        await _context.Stakeholders.AddAsync(stakeholder, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(Stakeholder stakeholder, CancellationToken token = default)
    { 
        _context.Stakeholders.Update(stakeholder);
        await _context.SaveChangesAsync(token);
    }
}