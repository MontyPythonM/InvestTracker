using Microsoft.AspNetCore.Authorization;

namespace InvestTracker.Shared.Infrastructure.Authorization;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(object permission) 
        : base(policy: permission.ToString() ?? throw new ArgumentException("HasPermission attribute cannot be empty."))
    {
    }
}