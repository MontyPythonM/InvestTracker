using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Notifications.Core.Events.External.Handlers;

internal sealed class AccountDeletedHandler : IEventHandler<AccountDeleted>
{
    private readonly IReceiverRepository _receiverRepository;

    public AccountDeletedHandler(IReceiverRepository receiverRepository)
    {
        _receiverRepository = receiverRepository;
    }

    public async Task HandleAsync(AccountDeleted @event)
    {
        var receiver = await _receiverRepository.GetAsync(@event.Id);

        if (receiver is null)
        {
            return;
        }

        await _receiverRepository.DeleteAsync(receiver);
    }
}