using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.Shared.Infrastructure.Context;

internal static class Extensions
{
    internal static IServiceCollection AddContext(this IServiceCollection services)
        => services
            .AddSingleton<IContextFactory, ContextFactory>()
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
            .AddTransient(serviceProvider => serviceProvider.GetRequiredService<IContextFactory>().Create());
}