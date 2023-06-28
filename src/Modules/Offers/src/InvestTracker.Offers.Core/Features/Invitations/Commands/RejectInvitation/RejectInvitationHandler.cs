﻿using InvestTracker.Offers.Core.Enums;
using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Offers.Core.Features.Invitations.Commands.RejectInvitation;

internal sealed class RejectInvitationHandler : ICommandHandler<RejectInvitation>
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly ITime _time;

    public RejectInvitationHandler(IInvitationRepository invitationRepository, ITime time)
    {
        _invitationRepository = invitationRepository;
        _time = time;
    }
    
    public async Task HandleAsync(RejectInvitation command, CancellationToken token)
    {
        var invitation = await _invitationRepository.GetAsync(command.Id, token);
        if (invitation is null)
        {
            throw new InvitationNotFoundException(command.Id);
        }

        invitation.Status = InvitationStatus.Rejected;
        invitation.StatusChangedAt = _time.Current();

        await _invitationRepository.UpdateAsync(invitation, token);
    }
}