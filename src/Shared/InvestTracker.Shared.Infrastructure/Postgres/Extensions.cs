using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.Shared.Infrastructure.Postgres;

public static class Extensions
{
    internal static IServiceCollection AddPostgresOptions(this IServiceCollection services)
    {
        var options = services.GetOptions<PostgresOptions>(PostgresOptions.SectionName);
        services.AddSingleton(options);

        return services;
    }
    
    public static IServiceCollection AddPostgres<T>(this IServiceCollection services, bool useLazyLoading = false)
        where T : DbContext
    {
        var options = services.GetOptions<PostgresOptions>(PostgresOptions.SectionName);
        services.AddDbContext<T>(option =>
            {
                option.UseNpgsql(options.ConnectionString);

                if (useLazyLoading)
                {
                    option.UseLazyLoadingProxies();
                }
            }
        );
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        return services;
    }
    
    public static IServiceCollection AddPostgres<T>(this IServiceCollection services, string connectionString, 
        bool useLazyLoading = false) where T : DbContext
    {
        services.AddDbContext<T>(option =>
            {
                option.UseNpgsql(connectionString);

                if (useLazyLoading)
                {
                    option.UseLazyLoadingProxies();
                }
            }
        );
        
        return services;
    }
}