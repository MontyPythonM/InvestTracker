using InvestTracker.Shared.Abstractions.Messages;
using InvestTracker.Shared.Infrastructure.Messages.Brokers;
using InvestTracker.Shared.Infrastructure.Messages.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.Shared.Infrastructure.Messages;

internal static class Extensions
{
    internal static IServiceCollection AddAsyncMessages(this IServiceCollection services)
        => services
            .AddSingleton<IMessageBroker, InMemoryMessageBroker>()
            .AddSingleton<IMessageChannel, MessageChannel>()
            .AddSingleton<IAsyncMessageDispatcher, AsyncMessageDispatcher>()
            .AddHostedService<BackgroundDispatcher>();
}