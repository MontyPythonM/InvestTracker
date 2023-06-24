using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Offers.Core.Commands.UpdateOffer;

internal sealed class UpdateOfferHandler : ICommandHandler<UpdateOffer>
{
    private readonly ITime _time;
    private readonly IOfferRepository _offerRepository;
    private readonly IAdvisorRepository _advisorRepository;

    public UpdateOfferHandler(ITime time, IOfferRepository offerRepository, IAdvisorRepository advisorRepository)
    {
        _time = time;
        _offerRepository = offerRepository;
        _advisorRepository = advisorRepository;
    }
    
    public async Task HandleAsync(UpdateOffer command, CancellationToken token)
    {
        var offer = await _offerRepository.GetAsync(command.Id, token);
        if (offer is null)
        {
            throw new OfferNotFoundException(command.Id);
        }

        offer.Update(command.Title, command.Description, command.Price, _time.Current(), command.Tags);
        await _offerRepository.UpdateAsync(offer, token);
    }
}