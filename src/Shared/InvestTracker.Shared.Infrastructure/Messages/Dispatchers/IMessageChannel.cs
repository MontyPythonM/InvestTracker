using System.Threading.Channels;
using InvestTracker.Shared.Abstractions.Messages;

namespace InvestTracker.Shared.Infrastructure.Messages.Dispatchers;

internal interface IMessageChannel
{
    ChannelReader<IMessage> Reader { get; }
    ChannelWriter<IMessage> Writer { get; }
}