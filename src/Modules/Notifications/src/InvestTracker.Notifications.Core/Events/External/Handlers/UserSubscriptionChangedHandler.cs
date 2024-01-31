using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

internal sealed class UserSubscriptionChangedHandler : IEventHandler<UserSubscriptionChanged>
{
    public async Task HandleAsync(UserSubscriptionChanged @event)
    {
        throw new NotImplementedException();
    }
}