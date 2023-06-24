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
    
    public static IServiceCollection AddPostgres<T>(this IServiceCollection services)
        where T : DbContext
    {
        var options = services.GetOptions<PostgresOptions>(PostgresOptions.SectionName);
        services.AddDbContext<T>(option => option.UseNpgsql(options.ConnectionString));
        
        return services;
    }
}