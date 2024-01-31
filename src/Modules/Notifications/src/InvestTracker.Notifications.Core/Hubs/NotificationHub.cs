using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace InvestTracker.Notifications.Core.Hubs;

[Authorize]
public class NotificationHub : Hub
{
}