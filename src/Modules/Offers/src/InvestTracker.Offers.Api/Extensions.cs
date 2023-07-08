using System.Runtime.CompilerServices;
using InvestTracker.Offers.Api.Permissions;
using InvestTracker.Offers.Core;
using InvestTracker.Shared.Abstractions.Authorization;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Bootstrapper")]
namespace InvestTracker.Offers.Api;

internal static class Extensions
{
    public static IServiceCollection AddOffersModule(this IServiceCollection services)
    {
        services.AddCore();
        services.AddSingleton<IModulePermissionMatrix, OffersPermissionMatrix>();
        
        return services;
    }
}