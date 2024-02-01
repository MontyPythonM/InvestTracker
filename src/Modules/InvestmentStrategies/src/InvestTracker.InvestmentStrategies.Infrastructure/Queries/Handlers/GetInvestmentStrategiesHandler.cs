using InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Dto;
using InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Queries;
using InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Queries.Handlers;

internal sealed class GetInvestmentStrategiesHandler : IQueryHandler<GetInvestmentStrategies, IEnumerable<InvestmentStrategiesDto>>
{
    private readonly InvestmentStrategiesDbContext _context;
    private readonly IRequestContext _requestContext;

    public GetInvestmentStrategiesHandler(InvestmentStrategiesDbContext context, IRequestContext requestContext)
    {
        _context = context;
        _requestContext = requestContext;
    }
    
    public async Task<IEnumerable<InvestmentStrategiesDto>> HandleAsync(GetInvestmentStrategies query, 
        CancellationToken token = default)
    {
        var currentUserId = new StakeholderId(_requestContext.Identity.UserId);
        
        var currentUserAssignedStrategies = await _context.InvestmentStrategies
            .AsNoTracking()
            .Where(strategy => strategy.Owner.Equals(currentUserId) || 
                               strategy.Collaborators.Select(c => c.CollaboratorId).Contains(currentUserId.Value))
            .ToListAsync(token);

        if (!currentUserAssignedStrategies.Any())
        {
            return new List<InvestmentStrategiesDto>();
        }

        var ownersOfCurrentUserAssignedStrategies = currentUserAssignedStrategies.Select(strategy => strategy.Owner);
        
        var owners = await _context.Stakeholders
            .AsNoTracking()
            .Where(stakeholder => ownersOfCurrentUserAssignedStrategies.Contains(stakeholder.Id))
            .ToListAsync(token);
        
        return currentUserAssignedStrategies.Select(strategy => new InvestmentStrategiesDto
        {
            Id = strategy.Id,
            Title = strategy.Title,
            OwnerId = strategy.Owner,
            OwnerName = GetOwnerNameById(strategy.Owner, owners)
        });
    }

    private static string GetOwnerNameById(StakeholderId stakeholderId, IEnumerable<Stakeholder> stakeholders)
    {
        var owner = stakeholders.FirstOrDefault(stakeholder => stakeholder.Id == stakeholderId);

        if (owner is null)
        {
            throw new StakeholderNotFoundException(stakeholderId);
        }

        return owner.FullName;
    }
}