using System.Security.Claims;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Infrastructure.Authentication;

namespace InvestTracker.Shared.Infrastructure.Context;

public class IdentityContext : IIdentityContext
{
    public bool IsAuthenticated { get; }
    public Guid UserId { get; }
    public string? Role { get; }
    public string? Subscription { get; }

    public IdentityContext(ClaimsPrincipal principal)
    {
        IsAuthenticated = principal.Identity?.IsAuthenticated is true;
        
        UserId = IsAuthenticated 
            ? Guid.Parse(principal.Identity.Name) 
            : Guid.Empty;
        
        Role = principal.Claims
            .SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        
        Subscription = principal.Claims
            .SingleOrDefault(x => x.Type == CustomClaim.Subscription)?.Value;
    }
}