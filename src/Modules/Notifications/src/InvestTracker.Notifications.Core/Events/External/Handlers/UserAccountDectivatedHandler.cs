using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

internal sealed class AccountDeactivatedHandler : IEventHandler<AccountDeactivated>
{
    public async Task HandleAsync(AccountDeactivated @event)
    {
        throw new NotImplementedException();
    }
}