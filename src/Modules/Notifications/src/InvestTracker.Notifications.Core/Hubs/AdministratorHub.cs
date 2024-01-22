using InvestTracker.Notifications.Core.Dto;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.AspNetCore.SignalR;

namespace InvestTracker.Notifications.Core.Hubs;

internal class AdministratorHub : Hub
{
    private readonly ITimeProvider _timeProvider;

    public AdministratorHub(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public async IAsyncEnumerable<PushNotification> SendPushNotification(CancellationToken token)
    {
        var push = new PushNotification(_timeProvider.Current(), "test message");
        
        yield return push;
    }
}