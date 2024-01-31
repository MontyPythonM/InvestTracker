using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

internal sealed class AccountDeletedHandler : IEventHandler<AccountDeleted>
{
    public async Task HandleAsync(AccountDeleted @event)
    {
        throw new NotImplementedException();
    }
}