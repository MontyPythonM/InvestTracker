using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Offers.Core.Features.Offers.Commands.CreateOffer;

internal sealed class CreateOfferHandler : ICommandHandler<CreateOffer>
{
    private readonly ITime _time;
    private readonly IOfferRepository _offerRepository;
    private readonly IAdvisorRepository _advisorRepository;

    public CreateOfferHandler(ITime time, IOfferRepository offerRepository, IAdvisorRepository advisorRepository)
    {
        _time = time;
        _offerRepository = offerRepository;
        _advisorRepository = advisorRepository;
    }
    
    public async Task HandleAsync(CreateOffer command, CancellationToken token)
    {
        var advisor = await _advisorRepository.GetAsync(command.AdvisorId, token);
        if (advisor is null)
        {
            throw new AdvisorNotFoundException(command.AdvisorId);
        }
        
        var offer = new Offer(command.Id, command.Title, command.Description, 
            command.Price, _time.Current(), advisor, command.Tags);

        await _offerRepository.CreateAsync(offer, token);
    }
}