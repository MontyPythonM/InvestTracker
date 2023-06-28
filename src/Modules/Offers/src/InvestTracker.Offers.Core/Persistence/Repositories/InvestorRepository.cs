using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Offers.Core.Persistence.Repositories;

internal class InvestorRepository : IInvestorRepository
{
    private readonly OffersDbContext _context;

    public InvestorRepository(OffersDbContext context)
    {
        _context = context;
    }
    
    public async Task<Investor?> GetAsync(Guid id, CancellationToken token)
        => await _context.Investors.SingleOrDefaultAsync(investor => investor.Id == id, token);

    public async Task CreateAsync(Investor investor, CancellationToken token)
    {
        await _context.Investors.AddAsync(investor, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken token)
        => await _context.Investors.AnyAsync(investor => investor.Id == id, token);
}