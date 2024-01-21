using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.IntegrationEvents;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.InvestmentStrategies.Application.Stakeholders.Events.External.Handlers;

internal sealed class UserSubscriptionChangedHandler : IEventHandler<UserSubscriptionChanged>
{
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly ITimeProvider _timeProvider;

    public UserSubscriptionChangedHandler(IStakeholderRepository stakeholderRepository, ITimeProvider timeProvider)
    {
        _stakeholderRepository = stakeholderRepository;
        _timeProvider = timeProvider;
    }

    public async Task HandleAsync(UserSubscriptionChanged @event)
    {
        var stakeholder = await _stakeholderRepository.GetAsync(@event.Id);
        if (stakeholder is null)
        {
            return;
        }

        stakeholder.SetSubscription(@event.Subscription, _timeProvider.Current(), @event.ModifiedBy);
        await _stakeholderRepository.UpdateAsync(stakeholder);
    }
}