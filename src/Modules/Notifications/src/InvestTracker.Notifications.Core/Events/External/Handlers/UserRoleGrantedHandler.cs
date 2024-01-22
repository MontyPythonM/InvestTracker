using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

internal sealed class UserRoleGrantedHandler : IEventHandler<UserRoleGranted>
{
    public async Task HandleAsync(UserRoleGranted @event)
    {
        throw new NotImplementedException();
    }
}