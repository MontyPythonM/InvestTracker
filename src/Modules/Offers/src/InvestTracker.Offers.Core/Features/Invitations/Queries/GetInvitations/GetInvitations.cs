using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.Offers.Core.Features.Invitations.Queries.GetInvitations;

internal record GetInvitations(Guid UserId) : IQuery<IEnumerable<InvitationDto>>;