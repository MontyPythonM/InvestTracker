﻿using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;

internal sealed class PortfolioRepository : IPortfolioRepository
{
    private readonly InvestmentStrategiesDbContext _context;

    public PortfolioRepository(InvestmentStrategiesDbContext context)
    {
        _context = context;
    }

    public async Task<Portfolio?> GetAsync(PortfolioId id, CancellationToken token = default)
    {
        return await _context.Portfolios
            .ApplyPortfolioIncludes()
            .SingleOrDefaultAsync(portfolio => portfolio!.Id == id, token);
    }
    
    public async Task<Portfolio?> GetAsync(PortfolioId id, bool asNoTracking = false, CancellationToken token = default)
    {
        return await _context.Portfolios
            .ApplyAsNoTracking(asNoTracking)
            .ApplyPortfolioIncludes()
            .SingleOrDefaultAsync(portfolio => portfolio!.Id == id, token);
    }
    
    public async Task<IEnumerable<Portfolio>> GetByInvestmentStrategyAsync(InvestmentStrategyId investmentStrategyId, 
        bool asNoTracking = false, CancellationToken token = default)
    {
        return await _context.Portfolios
            .ApplyAsNoTracking(asNoTracking)
            .ApplyPortfolioIncludes()
            .Where(portfolio => portfolio.InvestmentStrategyId == investmentStrategyId)
            .ToListAsync(token);
    }

    public async Task<IEnumerable<Portfolio>> GetOwnerPortfoliosAsync(StakeholderId ownerId, bool asNoTracking = false, 
        CancellationToken token = default)
    {
        var ownerPortfolios = await _context.InvestmentStrategies
            .ApplyAsNoTracking(asNoTracking)
            .Where(strategy => strategy.Owner == ownerId)
            .SelectMany(strategy => strategy.Portfolios)
            .ToListAsync(token);

        return await _context.Portfolios
            .ApplyAsNoTracking(asNoTracking)
            .Where(portfolio => ownerPortfolios.Select(p => p.PortfolioId).Contains(portfolio.Id.Value))
            .ToListAsync(token);
    }

    public async Task AddAsync(Portfolio portfolio, CancellationToken token = default)
    {
        await _context.Portfolios.AddAsync(portfolio, token);
        await _context.SaveChangesAsync(token);    
    }

    public async Task UpdateAsync(Portfolio portfolio, CancellationToken token = default)
    {
        _context.Portfolios.Update(portfolio);
        await _context.SaveChangesAsync(token);
    }

    public async Task<bool> HasAccessAsync(PortfolioId portfolioId, StakeholderId stakeholderId, CancellationToken token = default)
    {
        return await _context.InvestmentStrategies
            .AsNoTracking()
            .AnyAsync(strategy => strategy.Portfolios.Select(p => p.PortfolioId).Contains(portfolioId.Value) && 
                                  (strategy.Owner == stakeholderId || 
                                   strategy.IsShareEnabled && 
                                   strategy.Collaborators.Select(c => c.CollaboratorId).Contains(stakeholderId.Value)), token);
    }
}