using Microsoft.AspNetCore.Http;

namespace InvestTracker.Shared.Infrastructure.Api;

public class CorsHeaderMiddleware : IMiddleware
{
    private readonly CorsOptions _corsOptions;
    
    public CorsHeaderMiddleware(CorsOptions corsOptions)
    {
        _corsOptions = corsOptions;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context);
        
        if (context.Response.StatusCode == 401)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", string.Join(", ", _corsOptions.AllowedOrigins ?? []));
            context.Response.Headers.Add("Access-Control-Allow-Methods", string.Join(", ", _corsOptions.AllowedMethods ?? []));
            context.Response.Headers.Add("Access-Control-Allow-Headers", string.Join(", ", _corsOptions.AllowedHeaders ?? []));
            context.Response.Headers.Add("Access-Control-Allow-Credentials", _corsOptions.AllowCredentials ? "true" : "false");
        }
    }
}