﻿using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Offers.Core.Features.Offers.Commands.UpdateOffer;

internal sealed class UpdateOfferHandler : ICommandHandler<UpdateOffer>
{
    private readonly ITimeProvider _timeProvider;
    private readonly IOfferRepository _offerRepository;
    private readonly IRequestContext _requestContext;

    public UpdateOfferHandler(ITimeProvider timeProvider, IOfferRepository offerRepository, IRequestContext requestContext)
    {
        _timeProvider = timeProvider;
        _offerRepository = offerRepository;
        _requestContext = requestContext;
    }
    
    public async Task HandleAsync(UpdateOffer command, CancellationToken token)
    {
        var offer = await _offerRepository.GetAsync(command.Id, token);
        if (offer is null)
        {
            throw new OfferNotFoundException(command.Id);
        }

        if (offer.AdvisorId != _requestContext.Identity.UserId)
        {
            throw new IncorrectOfferOwnerException(command.Id);
        }

        offer.Update(command.Title, command.Description, command.Price, _timeProvider.Current(), command.Tags);
        await _offerRepository.UpdateAsync(offer, token);
    }
}