using System.Threading.Channels;
using InvestTracker.Notifications.Core.Dto.Notifications;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Notifications.Infrastructure.Dto;
using InvestTracker.Notifications.Infrastructure.Hubs;
using InvestTracker.Notifications.Infrastructure.Interfaces;
using InvestTracker.Notifications.Infrastructure.Options;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InvestTracker.Notifications.Infrastructure.Services.Notifications;

internal sealed class NotificationSender : BackgroundService, INotificationSender
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<NotificationSender> _logger;
    private readonly NotificationServiceOptions _options;
    private readonly Channel<Notification> _channel;

    public NotificationSender(IServiceProvider serviceProvider, ILogger<NotificationSender> logger, NotificationServiceOptions options)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _options = options;
        _channel = Channel.CreateUnbounded<Notification>();
    }

    public ValueTask SendAsync(Notification notification, CancellationToken token = default) 
        => _channel.Writer.WriteAsync(notification, token);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var notification in _channel.Reader.ReadAllAsync(stoppingToken))
        {
            if (_options.Enabled is false) return;
            
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var hub = scope.ServiceProvider.GetRequiredService<IHubContext<NotificationHub>>();
                var recipients = notification.Recipients.Select(r => r.ToString()).ToList();
                
                _logger.LogInformation($"Sending channel notification '{notification.Message}' to {string.Join(", ", recipients)}. Method: '{_options.MethodName}'.");

                await hub.Clients.Users(recipients).SendAsync(_options.MethodName, notification.Message, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}