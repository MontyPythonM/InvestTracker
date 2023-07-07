using InvestTracker.Shared.Abstractions.Modules;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InvestTracker.Shared.Infrastructure.Messages.Dispatchers;

internal sealed class BackgroundDispatcher : BackgroundService
{
    private readonly IMessageChannel _messageChannel;
    private readonly IModuleClient _moduleClient;
    private readonly ILogger<BackgroundDispatcher> _logger;

    public BackgroundDispatcher(IMessageChannel messageChannel, IModuleClient moduleClient,
        ILogger<BackgroundDispatcher> logger)
    {
        _messageChannel = messageChannel;
        _moduleClient = moduleClient;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var message in _messageChannel.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                await _moduleClient.PublishAsync(message);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }
        }
    }
}