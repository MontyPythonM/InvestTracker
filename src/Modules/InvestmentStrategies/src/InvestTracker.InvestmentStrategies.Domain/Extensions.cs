using System.Runtime.CompilerServices;
using InvestTracker.InvestmentStrategies.Domain.Asset.Policies.AssetLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.PortfolioLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.StrategyLimitPolicy;
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
            .AddSingleton<IAssetLimitPolicy, StandardInvestorAssetLimitPolicy>()
            .AddSingleton<IAssetLimitPolicy, ProfessionalInvestorAssetLimitPolicy>();

        services
            .AddSingleton<IStrategyLimitPolicy, AdvisorStrategyLimitPolicy>()
            .AddSingleton<IStrategyLimitPolicy, StandardInvestorStrategyLimitPolicy>()
            .AddSingleton<IStrategyLimitPolicy, ProfessionalInvestorStrategyLimitPolicy>();
        
        services
            .AddSingleton<IPortfolioLimitPolicy, AdvisorPortfolioLimitPolicy>()
            .AddSingleton<IPortfolioLimitPolicy, StandardInvestorPortfolioLimitPolicy>()
            .AddSingleton<IPortfolioLimitPolicy, ProfessionalInvestorPortfolioLimitPolicy>();
        
        return services;
    }
}