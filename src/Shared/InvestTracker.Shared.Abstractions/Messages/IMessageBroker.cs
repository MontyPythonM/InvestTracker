namespace InvestTracker.Shared.Abstractions.Messages;

public interface IMessageBroker
{
    Task PublishAsync(params IMessage[] messages);
}