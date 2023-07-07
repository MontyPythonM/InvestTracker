using System.Threading.Channels;
using InvestTracker.Shared.Abstractions.Messages;

namespace InvestTracker.Shared.Infrastructure.Messages.Dispatchers;

internal sealed class MessageChannel : IMessageChannel
{
    private readonly Channel<IMessage> _messages = Channel.CreateUnbounded<IMessage>();

    public ChannelReader<IMessage> Reader => _messages.Reader;
    public ChannelWriter<IMessage> Writer => _messages.Writer;
}