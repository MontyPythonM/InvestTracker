using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.Shared.Infrastructure.Authorization;

internal static class Extensions
{
    public static IServiceCollection AddPermissionAuthorization(this IServiceCollection services)
        => services
            .AddAuthorization()
            .AddScoped<PermissionInjectorMiddleware>()
            .AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>()
            .AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

    public static IApplicationBuilder UsePermissionsInjector(this IApplicationBuilder app)
        => app.UseMiddleware<PermissionInjectorMiddleware>();
}