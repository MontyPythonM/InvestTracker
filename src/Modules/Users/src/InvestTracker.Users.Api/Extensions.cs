using System.Runtime.CompilerServices;
using InvestTracker.Users.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Bootstrapper")]
namespace InvestTracker.Users.Api;

internal static class Extensions
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services)
    {
        services.AddCore();
        
        return services;
    }
}