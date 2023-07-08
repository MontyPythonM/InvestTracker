using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.Shared.Infrastructure.Authorization;

public class PermissionInjectorMiddleware : IMiddleware
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionInjectorMiddleware(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var actionDescriptor = context.GetEndpoint()?.Metadata.GetMetadata<ControllerActionDescriptor>();
        if (actionDescriptor is null)
        {
            return;
        }

        var assembly = actionDescriptor.ControllerTypeInfo.Assembly;

        var modulePermissionClasses = assembly
            .GetTypes()
            .Where(type => typeof(IModulePermissions).IsAssignableFrom(type) && !type.IsInterface);

        
        
        // pobrac z assembly zapytania wszystkie permissiony
        
        // podpiać do context.items te permissiony

        next.Invoke(context);
    }
}