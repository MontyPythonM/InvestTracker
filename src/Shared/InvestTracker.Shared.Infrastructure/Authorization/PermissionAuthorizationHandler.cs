using System.Collections;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.Shared.Infrastructure.Authorization;

public class PermissionAuthorizationHandler: AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var httpContext = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();

        var permissionsObj = httpContext.HttpContext?.Request.HttpContext.Items
            .Where(x => x.Key.Equals(CustomClaim.Permissions))
            .Select(x => x.Value)
            .FirstOrDefault();

        if (permissionsObj is null)
        {
            return Task.CompletedTask;
        }

        var permissions = ((IEnumerable)permissionsObj)
            .Cast<Permission>()
            .Select(permission => permission.PermissionName);

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
        
        return Task.CompletedTask;
    }
}