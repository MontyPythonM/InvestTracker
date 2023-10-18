using InvestTracker.Offers.Core.Persistence;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Offers.Core.Features.Collaborations.Queries.GetCollaboration;

internal sealed class GetCollaborationHandler : IQueryHandler<GetCollaboration, CollaborationDetailsDto?>
{
    private readonly OffersDbContext _context;
    private readonly Guid _currentUserId;

    public GetCollaborationHandler(OffersDbContext context, IRequestContext requestContext)
    {
        _context = context;
        _currentUserId = requestContext.Identity.UserId;
    }

    public async Task<CollaborationDetailsDto?> HandleAsync(GetCollaboration query, CancellationToken token = default)
        => await _context.Collaborations
            .AsNoTracking()
            .Select(collaboration => new CollaborationDetailsDto
            {
                Id = collaboration.Id,
                AdvisorId = collaboration.AdvisorId,
                InvestorId = collaboration.InvestorId,
                AdvisorFullName = collaboration.Advisor.FullName,
                AdvisorEmail = collaboration.Advisor.Email,
                AdvisorPhoneNumber = collaboration.Advisor.Email,
                InvestorFullName = collaboration.Investor.FullName,
                InvestorEmail = collaboration.Investor.Email,
                CreatedAt = collaboration.CreatedAt,
                CancelledAt = collaboration.CancelledAt,
                IsCancelled = collaboration.IsCancelled
            })
            .SingleOrDefaultAsync(c => c.AdvisorId == query.AdvisorId && 
                                       c.InvestorId == query.InvestorId &&
                                       (c.AdvisorId == _currentUserId || c.InvestorId == _currentUserId), token);
}