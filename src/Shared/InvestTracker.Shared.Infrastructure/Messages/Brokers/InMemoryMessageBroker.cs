using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Infrastructure.Messages.Dispatchers;

namespace InvestTracker.Shared.Infrastructure.Messages.Brokers;

internal sealed class InMemoryMessageBroker : IMessageBroker
{
    private readonly IAsyncMessageDispatcher _asyncMessageDispatcher;

    public InMemoryMessageBroker(IAsyncMessageDispatcher asyncMessageDispatcher)
    {
        _asyncMessageDispatcher = asyncMessageDispatcher;
    }
    
    public async Task PublishAsync(params IMessage[] messages)
    {
        if (messages is null)
        {
            return;
        }

        messages = messages
            .Where(message => message is not null)
            .ToArray();

        if (!messages.Any())
        {
            return;
        }

        foreach (var message in messages)
        {
            await _asyncMessageDispatcher.PublishAsync(message);
        }
    }
}