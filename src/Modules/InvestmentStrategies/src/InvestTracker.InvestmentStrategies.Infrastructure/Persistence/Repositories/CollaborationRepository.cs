using InvestTracker.InvestmentStrategies.Domain.Collaborations.Entities;
using InvestTracker.InvestmentStrategies.Domain.Collaborations.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;

internal sealed class CollaborationRepository : ICollaborationRepository
{
    private readonly InvestmentStrategiesDbContext _context;

    public CollaborationRepository(InvestmentStrategiesDbContext context)
    {
        _context = context;
    }

    public async Task<Collaboration?> GetAsync(StakeholderId advisorId, StakeholderId principalId, CancellationToken token = default)
    {
        return await _context.Collaborations
            .SingleOrDefaultAsync(collaboration => collaboration.AdvisorId == advisorId && 
                                                   collaboration.PrincipalId == principalId, token);
    }

    public async Task<bool> ExistsAsync(StakeholderId advisorId, StakeholderId principalId, CancellationToken token = default)
    {
        return await _context.Collaborations
            .AnyAsync(collaboration => collaboration.AdvisorId == advisorId && 
                                       collaboration.PrincipalId == principalId, token);
    }

    public async Task AddAsync(Collaboration collaboration, CancellationToken token = default)
    {
        await _context.Collaborations.AddAsync(collaboration, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(Collaboration collaboration, CancellationToken token = default)
    {
        _context.Collaborations.Update(collaboration);
        await _context.SaveChangesAsync(token);    
    }
}