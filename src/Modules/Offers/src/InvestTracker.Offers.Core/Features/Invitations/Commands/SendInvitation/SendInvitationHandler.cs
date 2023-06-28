using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Events;
using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.IntegrationEvents;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Offers.Core.Features.Invitations.Commands.SendInvitation;

internal sealed class SendInvitationHandler : ICommandHandler<SendInvitation>
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IInvitationRepository _invitationRepository;
    private readonly ITime _time;
    private readonly IOfferRepository _offerRepository;
    private readonly IInvestorRepository _investorRepository;

    public SendInvitationHandler(IEventDispatcher eventDispatcher, IInvitationRepository invitationRepository, 
        ITime time, IOfferRepository offerRepository, IInvestorRepository investorRepository)
    {
        _eventDispatcher = eventDispatcher;
        _invitationRepository = invitationRepository;
        _time = time;
        _offerRepository = offerRepository;
        _investorRepository = investorRepository;
    }
    
    public async Task HandleAsync(SendInvitation command, CancellationToken token)
    {
        var offer = await _offerRepository.GetAsync(command.OfferId, token);
        if (offer is null)
        {
            throw new OfferNotFoundException(command.OfferId);
        }

        var investor = await _investorRepository.GetAsync(command.SenderId, token);
        if (investor is null)
        {
            throw new InvestorNotFoundException(command.OfferId);
        }

        var invitation = new Invitation
        {
            Id = Guid.NewGuid(),
            SenderId = investor.Id,
            OfferId = offer.Id,
            Message = command.message,
            SentAt = _time.Current()
        };
        
        await _invitationRepository.CreateAsync(invitation, token);
        await _eventDispatcher.PublishAsync(new CollaborationInvitationSent(investor.Email, investor.FullName, 
            offer.Advisor.Email, offer.Advisor.FullName, offer.Title, command.message));
    }
}