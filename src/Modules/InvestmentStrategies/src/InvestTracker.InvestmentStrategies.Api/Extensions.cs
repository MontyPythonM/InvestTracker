using System.Runtime.CompilerServices;
using InvestTracker.InvestmentStrategies.Api.Permissions;
using InvestTracker.InvestmentStrategies.Application;
using InvestTracker.InvestmentStrategies.Domain;
using InvestTracker.InvestmentStrategies.Infrastructure;
using InvestTracker.Shared.Abstractions.Authorization;
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
        
        services.AddSingleton<IModulePermissionMatrix, InvestmentStrategiesPermissionMatrix>();
        
        return services;
    }
}