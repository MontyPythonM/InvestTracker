﻿using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Enums;
using InvestTracker.Offers.Core.Events;
using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Offers.Core.Features.Invitations.Commands.SendInvitation;

internal sealed class SendInvitationHandler : ICommandHandler<SendInvitation>
{
    private readonly IMessageBroker _messageBroker;
    private readonly IInvitationRepository _invitationRepository;
    private readonly ITimeProvider _timeProvider;
    private readonly IOfferRepository _offerRepository;
    private readonly IInvestorRepository _investorRepository;
    private readonly IRequestContext _requestContext;

    public SendInvitationHandler(IMessageBroker messageBroker, IInvitationRepository invitationRepository, 
        ITimeProvider timeProvider, IOfferRepository offerRepository, IInvestorRepository investorRepository, IRequestContext requestContext)
    {
        _messageBroker = messageBroker;
        _invitationRepository = invitationRepository;
        _timeProvider = timeProvider;
        _offerRepository = offerRepository;
        _investorRepository = investorRepository;
        _requestContext = requestContext;
    }
    
    public async Task HandleAsync(SendInvitation command, CancellationToken token)
    {
        var currentUser = _requestContext.Identity.UserId;
        var offer = await _offerRepository.GetAsync(command.OfferId, token);
        if (offer is null)
        {
            throw new OfferNotFoundException(command.OfferId);
        }

        if (offer.Invitations.Any(invitation => invitation.SenderId == currentUser &&
                                                invitation.Status == InvitationStatus.Expected))
        {
            throw new InvitationAlreadySentException(command.OfferId);
        }

        var investor = await _investorRepository.GetAsync(currentUser, token);
        if (investor is null)
        {
            throw new InvestorNotFoundException(command.OfferId);
        }

        var invitation = new Invitation
        {
            Id = Guid.NewGuid(),
            SenderId = investor.Id,
            OfferId = offer.Id,
            Message = command.Message,
            SentAt = _timeProvider.Current()
        };
        
        await _invitationRepository.CreateAsync(invitation, token);
        await _messageBroker.PublishAsync(new CollaborationInvitationSent(investor.Email, investor.FullName, 
            offer.Advisor.Email, offer.Advisor.FullName, offer.Title, command.Message));
    }
}