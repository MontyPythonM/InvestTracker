using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Offers.Core.Persistence.Repositories;

internal class CollaborationRepository : ICollaborationRepository
{
    private readonly OffersDbContext _context;

    public CollaborationRepository(OffersDbContext context)
    {
        _context = context;
    }
    
    public async Task<Collaboration?> GetAsync(Guid advisorId, Guid investorId, bool isCancelled = false, CancellationToken token = default)
        => await _context.Collaborations.SingleOrDefaultAsync(collaboration => 
            collaboration.AdvisorId == advisorId && 
            collaboration.InvestorId == investorId &&
            collaboration.IsCancelled == isCancelled, token);

    public async Task CreateAsync(Collaboration collaboration, CancellationToken token = default)
    {
        await _context.Collaborations.AddAsync(collaboration, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(Collaboration collaboration, CancellationToken token = default)
    {
        _context.Collaborations.Update(collaboration);
        await _context.SaveChangesAsync(token);
    }
}