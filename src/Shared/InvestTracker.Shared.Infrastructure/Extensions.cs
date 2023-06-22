using System.Reflection;
using System.Runtime.CompilerServices;
using InvestTracker.Shared.Infrastructure.Api;
using InvestTracker.Shared.Infrastructure.Commands;
using InvestTracker.Shared.Infrastructure.Exceptions;
using InvestTracker.Shared.Infrastructure.Queries;
using InvestTracker.Shared.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Bootstrapper")]
namespace InvestTracker.Shared.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IList<Assembly> assemblies)
    {
        services
            .AddExceptionHandling()
            .AddSwashbuckleSwagger()
            .AddQueries(assemblies)
            .AddCommands(assemblies);
            
        services
            .AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

        return services;
    }

    public static IApplicationBuilder UseSharedInfrastructure(this IApplicationBuilder app)
    {
        app.UseExceptionHandling();
        app.UseSwashbuckleSwagger();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", context => context.Response.WriteAsync("InvestTracker API"));
        });

        return app;
    }
}