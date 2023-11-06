﻿using System.Security.Claims;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
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
        var actionDescriptor = context.GetEndpoint()?.Metadata.GetMetadata<ControllerActionDescriptor>();
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
            .FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

        var userSubscription = context.User.Claims
            .FirstOrDefault(claim => claim.Type == CustomClaim.Subscription)?.Value;

        var currentUserPermissions = currentModulePermissions
            .Where(permission => permission.From == userRole ||
                                 permission.From == userSubscription);
        
        context.Items.Add(CustomClaim.Permissions, currentUserPermissions);
        await next.Invoke(context);
    }
}