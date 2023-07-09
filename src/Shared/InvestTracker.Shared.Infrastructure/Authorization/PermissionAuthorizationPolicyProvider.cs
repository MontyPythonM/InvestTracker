using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace InvestTracker.Shared.Infrastructure.Authorization;

public class PermissionAuthorizationPolicyProvider: DefaultAuthorizationPolicyProvider
{
    public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) 
        : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string permissionPolicy)
    {
        var policy = await base.GetPolicyAsync(permissionPolicy);

        if (policy is not null)
        {
            return policy;
        }
        
        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(permissionPolicy))
            .Build();
    }
}