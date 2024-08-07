﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.Shared.Infrastructure.Api;

public static class Extensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        var corsOptions = services.GetOptions<CorsOptions>(CorsOptions.SectionName);

        return services
            .AddSingleton(corsOptions)
            .AddCors(cors =>
            {
                var allowedHeaders = corsOptions.AllowedHeaders ?? [];
                var allowedMethods = corsOptions.AllowedMethods ?? [];
                var allowedOrigins = corsOptions.AllowedOrigins ?? [];
                
                cors.AddPolicy(CorsOptions.SectionName, builder =>
                {
                    var origins = allowedOrigins.ToArray();
                    if (corsOptions.AllowCredentials && origins.FirstOrDefault() != "*")
                    {
                        builder.AllowCredentials();
                    }
                    else
                    {
                        builder.DisallowCredentials();
                    }

                    builder
                        .WithOrigins(origins.ToArray())
                        .WithHeaders(allowedHeaders.ToArray())
                        .WithMethods(allowedMethods.ToArray())
                        .SetIsOriginAllowed((x) => true);
                });
            });
    }

    public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
        => app.UseCors(CorsOptions.SectionName);
    
    public static IServiceCollection AddCorsHeaderInjector(this IServiceCollection services)
        => services.AddScoped<CorsHeaderMiddleware>(); 
    
    public static IApplicationBuilder UseCorsHeaderInjector(this IApplicationBuilder app) 
        => app.UseMiddleware<CorsHeaderMiddleware>();
}