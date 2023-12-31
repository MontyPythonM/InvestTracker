﻿using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Offers.Core.Persistence.Repositories;

internal class AdvisorRepository : IAdvisorRepository
{
    private readonly OffersDbContext _context;

    public AdvisorRepository(OffersDbContext context)
    {
        _context = context;
    }

    public async Task<Advisor?> GetAsync(Guid advisorId, CancellationToken token = default)
        => await _context.Advisors.SingleOrDefaultAsync(advisor => advisor.Id == advisorId, token);

    public async Task CreateAsync(Advisor advisor, CancellationToken token = default)
    {
        await _context.Advisors.AddAsync(advisor, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(Advisor advisor, CancellationToken token = default)
    {
        _context.Advisors.Update(advisor);
        await _context.SaveChangesAsync(token);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken token = default)
        => await _context.Advisors.AnyAsync(advisor => advisor.Id == id, token);

    public async Task DeleteAsync(Advisor advisor, CancellationToken token = default)
    {
        _context.Advisors.Remove(advisor);
        await _context.SaveChangesAsync(token);
    }
}