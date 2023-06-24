using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Offers.Core.Persistence.Repositories;

internal class OfferRepository : IOfferRepository
{
    private readonly OffersDbContext _context;

    public OfferRepository(OffersDbContext context)
    {
        _context = context;
    }

    public async Task<Offer?> GetAsync(Guid offerId, CancellationToken token)
        => await _context.Offers.SingleOrDefaultAsync(offer => offer!.Id == offerId, token);

    public async Task CreateAsync(Offer offer, CancellationToken token)
    {
        await _context.Offers.AddAsync(offer, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(Offer offer, CancellationToken token)
    {
        _context.Offers.Update(offer);
        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(Offer offer, CancellationToken token)
    {
        _context.Offers.Remove(offer);
        await _context.SaveChangesAsync(token);
    }
}