using InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Dto;
using InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Queries;
using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.SharedExceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetInvestmentStrategyDetailsHandler : IQueryHandler<GetInvestmentStrategyDetails, InvestmentStrategyDetailsDto>
{
    private readonly InvestmentStrategiesDbContext _context;
    private readonly IResourceAccessor _resourceAccessor;
    
    public GetInvestmentStrategyDetailsHandler(InvestmentStrategiesDbContext context, IResourceAccessor resourceAccessor)
    {
        _context = context;
        _resourceAccessor = resourceAccessor;
    }
    
    public async Task<InvestmentStrategyDetailsDto> HandleAsync(GetInvestmentStrategyDetails query, 
        CancellationToken token = default)
    {
        var strategy = await _context.InvestmentStrategies
            .AsNoTracking()
            .SingleOrDefaultAsync(strategy => strategy.Id == query.InvestmentStrategyId, token);

        if (strategy is null)
        {
            throw new InvestmentStrategyNotFoundException(query.InvestmentStrategyId);
        }

        _resourceAccessor.Check(strategy);
        
        var stakeholders = await _context.Stakeholders
            .AsNoTracking()
            .Where(stakeholder => stakeholder.Id.Equals(strategy.Owner) || 
                                  strategy.Collaborators.Select(c => new StakeholderId(c.CollaboratorId)).Contains(stakeholder.Id))
            .ToListAsync(token);
        
        var portfolios = await _context.Portfolios
            .Where(portfolio => strategy.Portfolios.Select(p => new PortfolioId(p.PortfolioId)).Contains(portfolio.Id))
            .Select(portfolio => new PortfolioDto
            {
                PortfolioId = portfolio.Id,
                PortfolioTitle = portfolio.Title
            })
            .ToListAsync(token);
        
        return new InvestmentStrategyDetailsDto
        {
            Id = strategy.Id,
            Title = strategy.Title,
            OwnerId = strategy.Owner,
            OwnerName = GetOwnerNameById(strategy.Owner, stakeholders),
            Note = strategy.Note,
            IsShareEnabled = strategy.IsShareEnabled,
            Collaborators = GetCollaborators(strategy.Owner, stakeholders),
            Portfolios = portfolios
        };
    }
    
    private static string GetOwnerNameById(StakeholderId ownerId, IEnumerable<Stakeholder> stakeholders)
    {
        var owner = stakeholders.FirstOrDefault(stakeholder => stakeholder.Id == ownerId);

        if (owner is null)
        {
            throw new StakeholderNotFoundException(ownerId);
        }

        return owner.FullName;
    }

    private static IEnumerable<CollaboratorDto> GetCollaborators(StakeholderId ownerId,
        IEnumerable<Stakeholder> stakeholders)
    {
        return stakeholders
            .Where(stakeholder => stakeholder.Id != ownerId)
            .Select(stakeholder => new CollaboratorDto
            {
                CollaboratorId = stakeholder.Id,
                CollaboratorName = stakeholder.FullName
            });
    }
}