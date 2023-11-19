using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.Stakeholders.Events.External.Handlers;

internal sealed class UserSubscriptionChangedHandler : IEventHandler<UserSubscriptionChanged>
{
    private readonly IStakeholderRepository _stakeholderRepository;

    public UserSubscriptionChangedHandler(IStakeholderRepository stakeholderRepository)
    {
        _stakeholderRepository = stakeholderRepository;
    }

    public async Task HandleAsync(UserSubscriptionChanged @event)
    {
        var stakeholder = await _stakeholderRepository.GetAsync(@event.Id);
        if (stakeholder is null)
        {
            return;
        }

        stakeholder.SetSubscription(@event.Subscription);
        await _stakeholderRepository.UpdateAsync(stakeholder);
    }
}