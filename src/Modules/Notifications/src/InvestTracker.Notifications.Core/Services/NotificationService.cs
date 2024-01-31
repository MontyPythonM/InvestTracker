using System.Threading.Channels;
using InvestTracker.Notifications.Core.Dto;
using InvestTracker.Notifications.Core.Hubs;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Notifications.Core.Options;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InvestTracker.Notifications.Core.Services;

public class NotificationService : BackgroundService, INotificationPublisher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<NotificationService> _logger;
    private readonly NotificationServiceOptions _options;
    private readonly Channel<Notification> _channel;

    public NotificationService(IServiceProvider serviceProvider, ILogger<NotificationService> logger, NotificationServiceOptions options)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _options = options;
        _channel = Channel.CreateUnbounded<Notification>();
    }

    public ValueTask PublishAsync(Notification notification) => _channel.Writer.WriteAsync(notification);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_options.Enabled is false) return;
        
        do
        {
            try
            {
                var notification = await _channel.Reader.ReadAsync(stoppingToken);
                
                using var scope = _serviceProvider.CreateScope();
                var hub = scope.ServiceProvider.GetRequiredService<IHubContext<NotificationHub>>();
                
                var payload = new { Message = notification.Message };
                var recipients = notification.Recipients.Select(r => r.ToString()).ToList();
                
                _logger.LogInformation($"Sending channel notification '{notification.Message}' to {string.Join(", ", recipients)}. Method: '{_options.MethodName}'.");

                await hub.Clients.Users(recipients).SendAsync(_options.MethodName, notification.Message, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in notification service.");
            }
        } while (!stoppingToken.IsCancellationRequested);
    }
}