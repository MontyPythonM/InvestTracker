using System.Reflection;
using System.Runtime.CompilerServices;
using InvestTracker.Shared.Abstractions.Modules;
using InvestTracker.Shared.Abstractions.Time;
using InvestTracker.Shared.Infrastructure.Api;
using InvestTracker.Shared.Infrastructure.Authentication;
using InvestTracker.Shared.Infrastructure.Authorization;
using InvestTracker.Shared.Infrastructure.Commands;
using InvestTracker.Shared.Infrastructure.Context;
using InvestTracker.Shared.Infrastructure.DDD;
using InvestTracker.Shared.Infrastructure.Exceptions;
using InvestTracker.Shared.Infrastructure.IntegrationEvents;
using InvestTracker.Shared.Infrastructure.Messages;
using InvestTracker.Shared.Infrastructure.Modules;
using InvestTracker.Shared.Infrastructure.Postgres;
using InvestTracker.Shared.Infrastructure.Queries;
using InvestTracker.Shared.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TimeProvider = InvestTracker.Shared.Infrastructure.Time.TimeProvider;

[assembly: InternalsVisibleTo("InvestTracker.Bootstrapper")]
namespace InvestTracker.Shared.Infrastructure;

public static class Extensions
{
    internal static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IList<Assembly> assemblies, IList<IModule> modules)
    {
        services
            .AddContext()
            .AddSingleton<ITimeProvider, TimeProvider>()
            .AddExceptionHandling()
            .AddOpenApiDocumentation(modules)
            .AddModuleRequests(assemblies)
            .AddQueries(assemblies)
            .AddCommands(assemblies)
            .AddAsyncMessages()
            .AddIntegrationEvents(assemblies)
            .AddDomainEvents(assemblies)
            .AddAppAuthentication()
            .AddPermissionAuthorization()
            .AddCorsPolicy()
            .AddCorsHeaderInjector();
        
        services
            .AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

        return services;
    }

    internal static WebApplication UseSharedInfrastructure(this WebApplication app, IList<Assembly> assemblies, IList<IModule> modules)
    {
        app.UseExceptionHandling();
        app.UseOpenApiDocumentation(modules);
        app.UseAuthentication();
        app.UseRouting();
        app.UseCorsHeaderInjector();
        app.UsePermissionsInjector();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCorsPolicy();
        app.UseHttpsRedirection();

        if (app.Environment.IsDevelopment())
        {
            app.ApplyDatabaseMigrations(assemblies);
        }

        return app;
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