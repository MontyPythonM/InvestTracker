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
        if (context.Request.Method == HttpMethods.Options)
        {
            SetHeader(context.Response.Headers, "Access-Control-Allow-Origin", string.Join(", ", _corsOptions.AllowedOrigins ?? []));
            SetHeader(context.Response.Headers, "Access-Control-Allow-Headers", string.Join(", ", _corsOptions.AllowedHeaders ?? []));
            SetHeader(context.Response.Headers, "Access-Control-Allow-Headers", string.Join(", ", _corsOptions.AllowedHeaders ?? []));
            SetHeader(context.Response.Headers, "Access-Control-Allow-Credentials", _corsOptions.AllowCredentials ? "true" : "false");
            SetHeader(context.Response.Headers, "Content-Type", "application/json");
            context.Response.StatusCode = StatusCodes.Status200OK;
        }
        
        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized || context.Response.StatusCode == StatusCodes.Status403Forbidden)
        {
            SetHeader(context.Response.Headers, "Access-Control-Allow-Origin", string.Join(", ", _corsOptions.AllowedOrigins ?? []));
            SetHeader(context.Response.Headers, "Access-Control-Allow-Methods", string.Join(", ", _corsOptions.AllowedMethods ?? []));
            SetHeader(context.Response.Headers, "Access-Control-Allow-Headers", string.Join(", ", _corsOptions.AllowedHeaders ?? []));
            SetHeader(context.Response.Headers, "Access-Control-Allow-Credentials", _corsOptions.AllowCredentials ? "true" : "false");
        }
        
        await next(context);
    }
    
    private static void SetHeader(IHeaderDictionary headers, string key, string value)
    {
        if (headers.ContainsKey(key))
        {
            headers[key] = value;
            return;
        }
    
        headers.Add(key, value);
    }
}