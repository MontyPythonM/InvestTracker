using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

internal sealed class AccountActivatedHandler : IEventHandler<AccountActivated>
{
    public async Task HandleAsync(AccountActivated @event)
    {
        throw new NotImplementedException();
    }
}