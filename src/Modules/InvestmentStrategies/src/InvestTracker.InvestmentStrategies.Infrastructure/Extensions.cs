using System.Runtime.CompilerServices;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors;
using InvestTracker.InvestmentStrategies.Infrastructure.FileManagers;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Repositories;
using InvestTracker.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.InvestmentStrategies.Api")]
namespace InvestTracker.InvestmentStrategies.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddPostgres<InvestmentStrategiesDbContext>()
            .AddRepositories()
            .AddFileManagers()
            .AddDataCollectors();
    }
    
    public static T GetOptions<T>(this IServiceCollection services, string sectionName)
        where T : class, new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) 
        where T : class, new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
}