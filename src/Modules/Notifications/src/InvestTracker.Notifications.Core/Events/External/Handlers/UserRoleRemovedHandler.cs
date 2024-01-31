using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

internal sealed class UserRoleRemovedHandler : IEventHandler<UserRoleRemoved>
{
    public async Task HandleAsync(UserRoleRemoved @event)
    {
        throw new NotImplementedException();
    }
}