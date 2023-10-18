using InvestTracker.Offers.Core.Persistence;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Offers.Core.Features.Collaborations.Queries.GetCollaborations;

internal sealed class GetCollaborationsHandler : IQueryHandler<GetCollaborations, IEnumerable<CollaborationDto>>
{    
    private readonly OffersDbContext _context;

    public GetCollaborationsHandler(OffersDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<CollaborationDto>> HandleAsync(GetCollaborations query, CancellationToken token = default)
        => await _context.Collaborations
            .AsNoTracking()
            .Where(collaboration => collaboration.InvestorId == query.UserId || collaboration.AdvisorId == query.UserId)
            .Select(collaboration => new CollaborationDto
            {
                Id = collaboration.Id,
                AdvisorId = collaboration.AdvisorId,
                InvestorId = collaboration.InvestorId,
                AdvisorFullName = collaboration.Advisor.FullName,
                InvestorFullName = collaboration.Investor.FullName,
                CreatedAt = collaboration.CreatedAt,
                CancelledAt = collaboration.CancelledAt,
                IsCancelled = collaboration.IsCancelled
            })
            .ToListAsync(token);
}