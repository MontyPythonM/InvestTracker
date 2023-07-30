using System.Runtime.CompilerServices;
using InvestTracker.InvestmentStrategies.Domain.Asset.Policies;
using InvestTracker.InvestmentStrategies.Domain.Asset.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.InvestmentStrategies.Api")]
[assembly: InternalsVisibleTo("InvestTracker.InvestmentStrategies.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace InvestTracker.InvestmentStrategies.Domain;

internal static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services
            .AddSingleton<IAssetLimitPolicy, AdvisorAssetLimitPolicy>()
            .AddSingleton<IAssetLimitPolicy, ProfessionalInvestorAssetLimitPolicy>()
            .AddSingleton<IAssetLimitPolicy, StandardInvestorAssetLimitPolicy>();
        
        services.AddSingleton<IAssetPortfolioService, AssetPortfolioService>();
        
        return services;
    }
}