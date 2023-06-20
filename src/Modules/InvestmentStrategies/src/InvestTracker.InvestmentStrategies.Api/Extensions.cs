using System.Runtime.CompilerServices;
using InvestTracker.InvestmentStrategies.Application;
using InvestTracker.InvestmentStrategies.Domain;
using InvestTracker.InvestmentStrategies.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Bootstrapper")]
namespace InvestTracker.InvestmentStrategies.Api;

internal static class Extensions
{
    public static IServiceCollection AddInvestmentStrategiesModule(this IServiceCollection services)
    {
        services
            .AddDomain()
            .AddApplication()
            .AddInfrastructure();
        
        return services;
    }
}