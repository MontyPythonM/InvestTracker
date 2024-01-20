﻿using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Offers.Core.Events.External.Handlers;

internal sealed class UserAccountDeletedHandler : IEventHandler<UserAccountDeleted>
{
    private readonly IAdvisorRepository _advisorRepository;
    private readonly IOfferRepository _offerRepository;

    public UserAccountDeletedHandler(IAdvisorRepository advisorRepository, IOfferRepository offerRepository)
    {
        _advisorRepository = advisorRepository;
        _offerRepository = offerRepository;
    }

    public async Task HandleAsync(UserAccountDeleted @event)
    {
        var advisor = await _advisorRepository.GetAsync(@event.Id);

        if (advisor is null)
        {
            return;
        }
        
        await _offerRepository.DeleteAsync(advisor.Offers, CancellationToken.None);
    }
}