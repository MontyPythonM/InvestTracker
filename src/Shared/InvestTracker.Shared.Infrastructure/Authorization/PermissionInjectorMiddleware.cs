using System.Security.Claims;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.Shared.Infrastructure.Authorization;

internal sealed class PermissionInjectorMiddleware : IMiddleware
{
    private readonly IServiceProvider _serviceProvider;

    public PermissionInjectorMiddleware(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint is null)
        {
            return;
        }

        if (IsPermissionInjectorApplies(endpoint) is false)
        {
            await next.Invoke(context);
        }

        var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
        if (actionDescriptor is null)
        {
            return;
        }

        var currentAssembly = actionDescriptor.ControllerTypeInfo.Assembly;
        var permissionMatrices = _serviceProvider.GetServices<IModulePermissionMatrix>();

        var currentModulePermissions = permissionMatrices
            .Where(module => module.GetModuleName() == currentAssembly.GetName().Name)
            .SelectMany(module => module.Permissions)
            .ToHashSet();

        var userRole = context.User.Claims
            .FirstOrDefault(claim => claim.Type == CustomClaim.Role)?.Value;

        var userSubscription = context.User.Claims
            .FirstOrDefault(claim => claim.Type == CustomClaim.Subscription)?.Value;

        var currentUserPermissions = currentModulePermissions
            .Where(permission => permission.From == userRole ||
                                 permission.From == userSubscription);
        
        context.Items.Add(CustomClaim.Permissions, currentUserPermissions);
        await next.Invoke(context);
    }

    private static bool IsPermissionInjectorApplies(Endpoint endpoint)
    {
        if (endpoint is not RouteEndpoint routeEndpoint) return false;
        
        var controllerType = routeEndpoint.Metadata
            .OfType<ControllerActionDescriptor>()
            .FirstOrDefault()?
            .ControllerTypeInfo
            .AsType();

        return controllerType?.GetInterfaces().Any(i => i == typeof(IPermissionInjectable)) ?? false;
    }
}