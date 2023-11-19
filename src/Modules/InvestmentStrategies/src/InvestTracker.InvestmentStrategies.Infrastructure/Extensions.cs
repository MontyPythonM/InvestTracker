using System.Runtime.CompilerServices;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors;
using InvestTracker.InvestmentStrategies.Infrastructure.FileManagers;
using InvestTracker.InvestmentStrategies.Infrastructure.Options;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Interceptors;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;
using InvestTracker.Shared.Infrastructure;
using InvestTracker.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.InvestmentStrategies.Api")]
namespace InvestTracker.InvestmentStrategies.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var exchangeRateOptions = services.GetOptions<ExchangeRateOptions>("InvestmentStrategies:ExchangeRate");
        services.AddSingleton(exchangeRateOptions);
        services.AddSingleton<DomainEventsInterceptor>();
        
        return services
            .AddPostgres<InvestmentStrategiesDbContext>()
            .AddRepositories()
            .AddFileManagers()
            .AddDataCollectors();
    }
}