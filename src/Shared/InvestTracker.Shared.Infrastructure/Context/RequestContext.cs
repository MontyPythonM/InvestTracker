using InvestTracker.Shared.Abstractions.Context;
using Microsoft.AspNetCore.Http;

namespace InvestTracker.Shared.Infrastructure.Context;

internal class RequestContext : IRequestContext
{
    public string RequestId { get; } = $"{Guid.NewGuid():N}";
    public string TraceId { get; } = string.Empty;
    public IIdentityContext Identity { get; }
    
    private RequestContext()
    {
    }
    
    public static IRequestContext Empty => new RequestContext();
    
    public RequestContext(HttpContext context)
    {
        TraceId = context.TraceIdentifier;
        Identity = new IdentityContext(context.User);
    }
}