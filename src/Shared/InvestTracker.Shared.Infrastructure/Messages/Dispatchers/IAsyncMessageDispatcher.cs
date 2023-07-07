using InvestTracker.Shared.Abstractions.Messages;

namespace InvestTracker.Shared.Infrastructure.Messages.Dispatchers;

internal interface IAsyncMessageDispatcher
{
    Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
}