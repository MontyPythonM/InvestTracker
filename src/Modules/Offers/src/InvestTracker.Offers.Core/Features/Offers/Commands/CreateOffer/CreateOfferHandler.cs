using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Offers.Core.Features.Offers.Commands.CreateOffer;

internal sealed class CreateOfferHandler : ICommandHandler<CreateOffer>
{
    private readonly ITime _time;
    private readonly IOfferRepository _offerRepository;
    private readonly IAdvisorRepository _advisorRepository;
    private readonly IRequestContext _requestContext;

    public CreateOfferHandler(ITime time, IOfferRepository offerRepository, 
        IAdvisorRepository advisorRepository, IRequestContext requestContext)
    {
        _time = time;
        _offerRepository = offerRepository;
        _advisorRepository = advisorRepository;
        _requestContext = requestContext;
    }
    
    public async Task HandleAsync(CreateOffer command, CancellationToken token)
    {
        var currentUser = _requestContext.Identity.UserId;
        var advisor = await _advisorRepository.GetAsync(currentUser, token);
        if (advisor is null)
        {
            throw new AdvisorNotFoundException(currentUser);
        }
        
        var offer = new Offer(Guid.NewGuid(), command.Title, command.Description, 
            command.Price, _time.Current(), advisor, command.Tags);

        await _offerRepository.CreateAsync(offer, token);
    }
}