using InvestTracker.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.Shared.Infrastructure.Exceptions;

internal static class Extensions
{
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
        => services
            .AddScoped<ExceptionHandlerMiddleware>()
            .AddSingleton<IExceptionToResponse, ExceptionToResponse>();
    
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionHandlerMiddleware>();
}