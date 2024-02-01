using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Offers.Core.Events.External.Handlers;

internal sealed class AccountCreatedHandler : IEventHandler<AccountCreated>
{
    private readonly IInvestorRepository _investorRepository;
    private readonly IAdvisorRepository _advisorRepository;

    public AccountCreatedHandler(IInvestorRepository investorRepository, IAdvisorRepository advisorRepository)
    {
        _investorRepository = investorRepository;
        _advisorRepository = advisorRepository;
    }
    
    public async Task HandleAsync(AccountCreated @event)
    {
        if (@event.Subscription == SystemSubscription.Advisor)
        {
            if (await _advisorRepository.ExistsAsync(@event.Id))
            {
                return;
            }

            var advisor = new Advisor(@event.Id, @event.FullName, @event.Email);
            await _advisorRepository.CreateAsync(advisor);
            return;
        }

        if (await _investorRepository.ExistsAsync(@event.Id))
        {
            return;
        }

        var investor = new Investor(@event.Id, @event.FullName, @event.Email);
        await _investorRepository.CreateAsync(investor);
    }
}