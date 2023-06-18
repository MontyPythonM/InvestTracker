using System.Runtime.CompilerServices;
using InvestTracker.Offers.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Bootstrapper")]
namespace InvestTracker.Offers.Api;

internal static class Extensions
{
    public static IServiceCollection AddOffersModule(this IServiceCollection services)
    {
        services.AddCore();
        
        return services;
    }
}