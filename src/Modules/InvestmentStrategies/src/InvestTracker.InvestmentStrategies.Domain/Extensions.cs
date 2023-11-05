using System.Runtime.CompilerServices;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Policies.AssetLimitPolicy;
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
            .AddPolicies();
        
        return services;
    }

    private static IServiceCollection AddPolicies(this IServiceCollection services)
        => services
            .AddSingleton<IAssetTypeLimitPolicy, NoneAssetTypeLimitPolicy>()
            .AddSingleton<IAssetTypeLimitPolicy, AdvisorAssetTypeLimitPolicy>()
            .AddSingleton<IAssetTypeLimitPolicy, StandardInvestorAssetTypeLimitPolicy>()
            .AddSingleton<IAssetTypeLimitPolicy, ProfessionalInvestorAssetTypeLimitPolicy>()
            .AddSingleton<IStrategyLimitPolicy, NoneStrategyLimitPolicy>()
            .AddSingleton<IStrategyLimitPolicy, AdvisorStrategyLimitPolicy>()
            .AddSingleton<IStrategyLimitPolicy, StandardInvestorStrategyLimitPolicy>()
            .AddSingleton<IStrategyLimitPolicy, ProfessionalInvestorStrategyLimitPolicy>()
            .AddSingleton<IPortfolioLimitPolicy, NonePortfolioLimitPolicy>()
            .AddSingleton<IPortfolioLimitPolicy, AdvisorPortfolioLimitPolicy>()
            .AddSingleton<IPortfolioLimitPolicy, StandardInvestorPortfolioLimitPolicy>()
            .AddSingleton<IPortfolioLimitPolicy, ProfessionalInvestorPortfolioLimitPolicy>();
}