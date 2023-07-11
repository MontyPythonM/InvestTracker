﻿using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Enums;
using InvestTracker.Offers.Core.Events;
using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Offers.Core.Features.Invitations.Commands.ConfirmInvitation;

internal sealed class ConfirmInvitationHandler : ICommandHandler<ConfirmInvitation>
{
    private readonly IMessageBroker _messageBroker;
    private readonly IInvitationRepository _invitationRepository;
    private readonly ICollaborationRepository _collaborationRepository;
    private readonly ITime _time;
    private readonly IContext _context;

    public ConfirmInvitationHandler(IMessageBroker messageBroker, IInvitationRepository invitationRepository,
        ICollaborationRepository collaborationRepository, ITime time, IContext context)
    {
        _messageBroker = messageBroker;
        _invitationRepository = invitationRepository;
        _collaborationRepository = collaborationRepository;
        _time = time;
        _context = context;
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

        if (_context.Identity.UserId != advisorId)
        {
            throw new CannotConfirmNotOwnCollaborationsException();
        }
        
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
        await _messageBroker.PublishAsync(new CollaborationStarted(advisorId, investorId));
    }
}