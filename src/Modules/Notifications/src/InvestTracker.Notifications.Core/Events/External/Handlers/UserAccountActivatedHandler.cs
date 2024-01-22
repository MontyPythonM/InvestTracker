using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

internal sealed class UserAccountActivatedHandler : IEventHandler<UserAccountActivated>
{
    public async Task HandleAsync(UserAccountActivated @event)
    {
        throw new NotImplementedException();
    }
}