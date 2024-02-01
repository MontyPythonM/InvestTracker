using InvestTracker.Notifications.Core.Interfaces;

namespace InvestTracker.Notifications.Core.Services;

internal sealed class ReceiverSynchronizer : IReceiverSynchronizer
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly HttpClient _httpClient;

    public ReceiverSynchronizer(IReceiverRepository receiverRepository, HttpClient httpClient)
    {
        _receiverRepository = receiverRepository;
        _httpClient = httpClient;
    }

    public async Task Synchronize(CancellationToken token)
    {
        // TODO Synchronize
        // get users from users module
        // add missing receivers to notifications module database
        // check and update existing receivers data
    }
}