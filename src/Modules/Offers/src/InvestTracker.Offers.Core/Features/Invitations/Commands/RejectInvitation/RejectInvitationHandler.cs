using InvestTracker.Offers.Core.Enums;
using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Offers.Core.Features.Invitations.Commands.RejectInvitation;

internal sealed class RejectInvitationHandler : ICommandHandler<RejectInvitation>
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly ITimeProvider _timeProvider;
    private readonly IRequestContext _requestContext;

    public RejectInvitationHandler(IInvitationRepository invitationRepository, ITimeProvider timeProvider, IRequestContext requestContext)
    {
        _invitationRepository = invitationRepository;
        _timeProvider = timeProvider;
        _requestContext = requestContext;
    }
    
    public async Task HandleAsync(RejectInvitation command, CancellationToken token)
    {
        var invitation = await _invitationRepository.GetAsync(command.Id, token);
        if (invitation is null)
        {
            throw new InvitationNotFoundException(command.Id);
        }
        
        if (_requestContext.Identity.UserId != invitation.Offer.AdvisorId)
        {
            throw new CannotConfirmNotOwnCollaborationsException();
        }

        if (invitation.Status != InvitationStatus.Expected)
        {
            throw new InvitationDecisionMadeException(invitation.Id);
        }

        invitation.Status = InvitationStatus.Rejected;
        invitation.StatusChangedAt = _timeProvider.Current();

        await _invitationRepository.UpdateAsync(invitation, token);
    }
}