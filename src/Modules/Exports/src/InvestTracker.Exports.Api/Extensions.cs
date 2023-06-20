using System.Runtime.CompilerServices;
using InvestTracker.Exports.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Bootstrapper")]
namespace InvestTracker.Exports.Api;

internal static class Extensions
{
    public static IServiceCollection AddExportsModule(this IServiceCollection services)
    {
        services.AddCore();
        
        return services;
    }
}