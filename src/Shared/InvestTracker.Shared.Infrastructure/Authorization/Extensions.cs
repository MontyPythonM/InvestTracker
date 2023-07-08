using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.Shared.Infrastructure.Authorization;

internal static class Extensions
{
    public static IServiceCollection AddPermissions(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddScoped<PermissionInjectorMiddleware>();
        return services;
    }
    
    public static IApplicationBuilder UsePermissionsInjector(this IApplicationBuilder app)
        => app.UseMiddleware<PermissionInjectorMiddleware>();
}