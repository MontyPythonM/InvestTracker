using System.Runtime.CompilerServices;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Policies.StrategyLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.AssetTypeLimitPolicy;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Policies.PortfolioLimitPolicy;
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
            .AddSingleton<IFinancialAssetLimitPolicy, NoneFinancialAssetLimitPolicy>()
            .AddSingleton<IFinancialAssetLimitPolicy, AdvisorFinancialAssetLimitPolicy>()
            .AddSingleton<IFinancialAssetLimitPolicy, StandardInvestorFinancialAssetLimitPolicy>()
            .AddSingleton<IFinancialAssetLimitPolicy, ProfessionalInvestorFinancialAssetLimitPolicy>()
            .AddSingleton<IStrategyLimitPolicy, NoneStrategyLimitPolicy>()
            .AddSingleton<IStrategyLimitPolicy, AdvisorStrategyLimitPolicy>()
            .AddSingleton<IStrategyLimitPolicy, StandardInvestorStrategyLimitPolicy>()
            .AddSingleton<IStrategyLimitPolicy, ProfessionalInvestorStrategyLimitPolicy>()
            .AddSingleton<IPortfolioLimitPolicy, NonePortfolioLimitPolicy>()
            .AddSingleton<IPortfolioLimitPolicy, AdvisorPortfolioLimitPolicy>()
            .AddSingleton<IPortfolioLimitPolicy, StandardInvestorPortfolioLimitPolicy>()
            .AddSingleton<IPortfolioLimitPolicy, ProfessionalInvestorPortfolioLimitPolicy>();
}