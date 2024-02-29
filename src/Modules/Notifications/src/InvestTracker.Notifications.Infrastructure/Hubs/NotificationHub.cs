using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace InvestTracker.Notifications.Infrastructure.Hubs;

[Authorize]
public class NotificationHub : Hub
{
}