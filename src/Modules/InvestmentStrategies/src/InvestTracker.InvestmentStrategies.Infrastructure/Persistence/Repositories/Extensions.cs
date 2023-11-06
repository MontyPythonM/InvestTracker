using InvestTracker.InvestmentStrategies.Domain.Collaborations.Repositories;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Repositories;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;

internal static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
            .AddScoped<IInvestmentStrategyRepository, InvestmentStrategyRepository>()
            .AddScoped<IStakeholderRepository, StakeholderRepository>()
            .AddScoped<ICollaborationRepository, CollaborationRepository>()
            .AddScoped<IFinancialAssetRepository, FinancialAssetRepository>();
}