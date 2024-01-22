using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

internal sealed class UserAccountDeletedHandler : IEventHandler<UserAccountDeleted>
{
    public async Task HandleAsync(UserAccountDeleted @event)
    {
        throw new NotImplementedException();
    }
}