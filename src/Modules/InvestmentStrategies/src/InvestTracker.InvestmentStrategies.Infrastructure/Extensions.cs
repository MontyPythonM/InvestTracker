using System.Runtime.CompilerServices;
using InvestTracker.InvestmentStrategies.Domain.Common;
using InvestTracker.InvestmentStrategies.Infrastructure.Authorization;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors;
using InvestTracker.InvestmentStrategies.Infrastructure.FileManagers;
using InvestTracker.InvestmentStrategies.Infrastructure.Options;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Interceptors;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;
using InvestTracker.InvestmentStrategies.Infrastructure.Services;
using InvestTracker.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.InvestmentStrategies.Api")]
namespace InvestTracker.InvestmentStrategies.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DomainEventsInterceptor>();
        services.AddScoped<IResourceAccessor, ResourceAccessor>();

        return services
            .AddPostgres<InvestmentStrategiesDbContext>(useAuditableEntities: true)
            .AddRepositories()
            .AddFileManagers()
            .AddDataCollectors()
            .AddServices()
            .AddAppSettingsOptions();
    }
}