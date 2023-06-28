using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.Offers.Core.Features.Invitations.Queries.GetInvitations;

internal sealed class GetInvitationsHandler : IQueryHandler<GetInvitations, IEnumerable<InvitationDto>>
{
    public async Task<IEnumerable<InvitationDto>> HandleAsync(GetInvitations query, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}