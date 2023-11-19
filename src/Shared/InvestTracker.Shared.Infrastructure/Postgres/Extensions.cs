using InvestTracker.Shared.Infrastructure.Postgres.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.Shared.Infrastructure.Postgres;

public static class Extensions
{
    public static IServiceCollection AddPostgres<T>(this IServiceCollection services, bool useLazyLoading = false, 
        bool useAuditableEntities = false) where T : DbContext
    {
        if (useAuditableEntities)
            services.AddSingleton<AuditableEntitiesInterceptor>();
        
        services.AddDbContext<T>(option =>
            {
                using var serviceProvider = services.BuildServiceProvider();
                var options = services.GetOptions<PostgresOptions>(PostgresOptions.SectionName);
                
                option.UseNpgsql(options.ConnectionString);

                if (useLazyLoading)
                    option.UseLazyLoadingProxies();
                
                if (useAuditableEntities)
                {
                    var auditableInterceptor = serviceProvider.GetRequiredService<AuditableEntitiesInterceptor>();
                    option.AddInterceptors(auditableInterceptor);
                }
            }
        );
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        return services;
    }
}