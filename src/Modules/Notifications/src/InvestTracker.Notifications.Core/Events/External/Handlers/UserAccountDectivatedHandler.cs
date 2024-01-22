using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

internal sealed class UserAccountDeactivatedHandler : IEventHandler<UserAccountDeactivated>
{
    public async Task HandleAsync(UserAccountDeactivated @event)
    {
        throw new NotImplementedException();
    }
}