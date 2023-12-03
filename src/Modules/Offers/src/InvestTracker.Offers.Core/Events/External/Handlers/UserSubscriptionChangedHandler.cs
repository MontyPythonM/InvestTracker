using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Offers.Core.Events.External.Handlers;

internal sealed class UserSubscriptionChangedHandler : IEventHandler<UserSubscriptionChanged>
{
    private readonly IAdvisorRepository _advisorRepository;
    private readonly IInvestorRepository _investorRepository;

    public UserSubscriptionChangedHandler(IAdvisorRepository advisorRepository, IInvestorRepository investorRepository)
    {
        _advisorRepository = advisorRepository;
        _investorRepository = investorRepository;
    }
    
    public async Task HandleAsync(UserSubscriptionChanged @event)
    {
        var investor = await _investorRepository.GetAsync(@event.Id);
        if (investor is not null)
        {
            // investor -> advisor
            if (@event.Subscription is SystemSubscription.Advisor)
            {
                var newAdvisor = new Advisor(investor.Id, investor.Email, investor.FullName);
                await _advisorRepository.CreateAsync(newAdvisor);
                await _investorRepository.DeleteAsync(investor);
                return;
            }

            // investor -> none
            if (@event.Subscription is SystemSubscription.None)
            {
                await _investorRepository.DeleteAsync(investor);
                return;
            }
        }

        var advisor = await _advisorRepository.GetAsync(@event.Id);
        if (advisor is not null)
        {
            // advisor -> none
            if (@event.Subscription is SystemSubscription.None)
            {
                await _advisorRepository.DeleteAsync(advisor);
                return;
            }
            
            // advisor -> investor
            if (@event.Subscription is SystemSubscription.StandardInvestor or SystemSubscription.ProfessionalInvestor)
            {
                var newInvestor = new Investor(advisor.Id, advisor.FullName, advisor.Email);
                await _investorRepository.CreateAsync(newInvestor);
                await _advisorRepository.DeleteAsync(advisor);
            }
        }

        if (investor is null && advisor is null && @event.Subscription is not SystemSubscription.None)
        {
            if (@event.Subscription is SystemSubscription.Advisor)
            {
                var newAdvisor = new Advisor(@event.Id, @event.FullName, @event.Email);
                await _advisorRepository.CreateAsync(newAdvisor);
                return;
            }

            var newInvestor = new Investor(@event.Id, @event.FullName, @event.Email);
            await _investorRepository.CreateAsync(newInvestor);
        }
    }
}