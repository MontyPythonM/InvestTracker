using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Offers.Core.Persistence.Repositories;

internal class InvitationRepository : IInvitationRepository
{
    private readonly OffersDbContext _context;

    public InvitationRepository(OffersDbContext context)
    {
        _context = context;
    }

    public async Task<Invitation?> GetAsync(Guid id, CancellationToken token)
        => await _context.Invitations.SingleOrDefaultAsync(invitation => invitation.Id == id, token);

    public async Task CreateAsync(Invitation invitation, CancellationToken token)
    {
        await _context.Invitations.AddAsync(invitation, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(Invitation invitation, CancellationToken token)
    {
        _context.Invitations.Update(invitation);
        await _context.SaveChangesAsync(token);
    }
    
    public async Task<bool> ExistsAsync(Guid id, CancellationToken token)
        => await _context.Investors.AnyAsync(invitation => invitation.Id == id, token);
}