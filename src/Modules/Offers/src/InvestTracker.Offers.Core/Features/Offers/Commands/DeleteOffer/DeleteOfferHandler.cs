using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;

namespace InvestTracker.Offers.Core.Features.Offers.Commands.DeleteOffer;

internal sealed class DeleteOfferHandler : ICommandHandler<DeleteOffer>
{
    private readonly IOfferRepository _offerRepository;
    private readonly IRequestContext _requestContext;

    public DeleteOfferHandler(IOfferRepository offerRepository, IRequestContext requestContext)
    {
        _offerRepository = offerRepository;
        _requestContext = requestContext;
    }
    
    public async Task HandleAsync(DeleteOffer command, CancellationToken token)
    {
        var offer = await _offerRepository.GetAsync(command.Id, token);
        if (offer is null)
        {
            throw new OfferNotFoundException(command.Id);
        }

        var currentUser = _requestContext.Identity;
        if (!offer.IsOwner(currentUser.UserId) && !SystemRole.IsAdministrator(currentUser.Role))
        {
            throw new OfferAccessException(offer.Id);
        }
        
        await _offerRepository.DeleteAsync(offer, token);
    }
}