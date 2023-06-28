﻿using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Enums;
using InvestTracker.Offers.Core.Events;
using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.IntegrationEvents;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Offers.Core.Features.Invitations.Commands.ConfirmInvitation;

internal sealed class ConfirmInvitationHandler : ICommandHandler<ConfirmInvitation>
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IInvitationRepository _invitationRepository;
    private readonly ICollaborationRepository _collaborationRepository;
    private readonly ITime _time;

    public ConfirmInvitationHandler(IEventDispatcher eventDispatcher, IInvitationRepository invitationRepository,
        ICollaborationRepository collaborationRepository, ITime time)
    {
        _eventDispatcher = eventDispatcher;
        _invitationRepository = invitationRepository;
        _collaborationRepository = collaborationRepository;
        _time = time;
    }
    
    public async Task HandleAsync(ConfirmInvitation command, CancellationToken token)
    {
        var invitation = await _invitationRepository.GetAsync(command.Id, token);
        if (invitation is null)
        {
            throw new InvitationNotFoundException(command.Id);
        }

        var investorId = invitation.SenderId;
        var advisorId = invitation.Offer.AdvisorId;

        var collaboration = new Collaboration
        {
            AdvisorId = advisorId,
            InvestorId = investorId,
            CreatedAt = _time.Current()
        };

        invitation.Status = InvitationStatus.Accepted;
        invitation.StatusChangedAt = _time.Current();

        await _invitationRepository.UpdateAsync(invitation, token);
        await _collaborationRepository.CreateAsync(collaboration, token);
        await _eventDispatcher.PublishAsync(new CollaborationStarted(advisorId, investorId));
    }
}