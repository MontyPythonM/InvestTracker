using System.Runtime.CompilerServices;
using InvestTracker.Calculators.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Bootstrapper")]
namespace InvestTracker.Calculators.Api;

internal static class Extensions
{
    public static IServiceCollection AddCalculatorsModule(this IServiceCollection services)
    {
        services.AddCore();
        
        return services;
    }
}