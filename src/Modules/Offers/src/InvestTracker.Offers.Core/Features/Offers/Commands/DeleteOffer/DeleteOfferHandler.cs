using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.Offers.Core.Features.Offers.Commands.DeleteOffer;

internal sealed class DeleteOfferHandler : ICommandHandler<DeleteOffer>
{
    private readonly IOfferRepository _offerRepository;

    public DeleteOfferHandler(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }
    
    public async Task HandleAsync(DeleteOffer command, CancellationToken token)
    {
        var offer = await _offerRepository.GetAsync(command.Id, token);
        if (offer is null)
        {
            throw new OfferNotFoundException(command.Id);
        }

        await _offerRepository.DeleteAsync(offer, token);
    }
}