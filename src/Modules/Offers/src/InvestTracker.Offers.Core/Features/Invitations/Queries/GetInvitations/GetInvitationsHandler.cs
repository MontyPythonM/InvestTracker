using InvestTracker.Offers.Core.Persistence;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Offers.Core.Features.Invitations.Queries.GetInvitations;

internal sealed class GetInvitationsHandler : IQueryHandler<GetInvitations, IEnumerable<InvitationDto>>
{
    private readonly OffersDbContext _context;

    public GetInvitationsHandler(OffersDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InvitationDto>> HandleAsync(GetInvitations query, CancellationToken token = default)
        => await _context.Invitations
            .AsNoTracking()
            .Where(invitation => invitation.SenderId == query.UserId || invitation.Offer.AdvisorId == query.UserId) 
            .Select(invitation => new InvitationDto
            {
                Id = invitation.Id,
                SenderId = invitation.SenderId,
                SenderFullName = invitation.Sender.FullName,
                AdvisorId = invitation.Offer.AdvisorId,
                AdvisorFullName = invitation.Offer.Advisor.FullName,
                OfferId = invitation.OfferId,
                OfferTitle = invitation.Offer.Title,
                Message = invitation.Message,
                SentAt = invitation.SentAt,
                StatusChangedAt = invitation.StatusChangedAt,
                Status = invitation.Status
            })
            .ToListAsync(token);
}