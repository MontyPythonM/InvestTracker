using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Notifications.Api")]
namespace InvestTracker.Notifications.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {

        return services;
    }
}