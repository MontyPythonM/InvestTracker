using InvestTracker.Shared.Abstractions.Context;
using Microsoft.AspNetCore.Http;

namespace InvestTracker.Shared.Infrastructure.Context;

internal sealed class ContextFactory : IContextFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ContextFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public IRequestContext Create()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        return httpContext is null ? RequestContext.Empty : new RequestContext(httpContext);
    }
}