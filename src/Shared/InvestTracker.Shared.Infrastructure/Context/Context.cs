using InvestTracker.Shared.Abstractions.Context;
using Microsoft.AspNetCore.Http;

namespace InvestTracker.Shared.Infrastructure.Context;

internal class Context : IContext
{
    public string RequestId { get; } = $"{Guid.NewGuid():N}";
    public string TraceId { get; } = string.Empty;
    public IIdentityContext Identity { get; }
    
    private Context()
    {
    }
    
    public static IContext Empty => new Context();
    
    public Context(HttpContext context)
    {
        TraceId = context.TraceIdentifier;
        Identity = new IdentityContext(context.User);
    }
}