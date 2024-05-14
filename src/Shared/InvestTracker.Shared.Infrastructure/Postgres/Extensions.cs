using System.Reflection;
using InvestTracker.Shared.Abstractions.Exceptions;
using InvestTracker.Shared.Infrastructure.Postgres.Interceptors;
using Microsoft.AspNetCore.Builder;
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

    public static WebApplication ApplyDatabaseMigrations(this WebApplication app, IList<Assembly> assemblies)
    {
            using var scope = app.Services.CreateScope();
            
            var dbContextTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => !type.IsAbstract && !type.IsGenericType && type.IsSubclassOf(typeof(DbContext)))
                .ToList();

            foreach (var type in dbContextTypes)
            {
                var dbContext = scope.ServiceProvider.GetService(type) as DbContext;
                
                if (dbContext is null)
                    continue;

                if (!dbContext.Database.CanConnect())
                    throw new DatabaseConnectionException($"Cannot connect to database with connection string: {dbContext.Database.GetDbConnection().ConnectionString}"); 

                dbContext.Database.Migrate();
            }

            return app;
    }
}