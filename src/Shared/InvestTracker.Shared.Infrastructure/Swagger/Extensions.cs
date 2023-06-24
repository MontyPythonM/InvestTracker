using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace InvestTracker.Shared.Infrastructure.Swagger;

internal static class Extensions
{
   private const string AppName = "InvestTracker API";
   
   public static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services)
   {
      services.AddSwaggerGen(options =>
      {
         options.EnableAnnotations();
         
         options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         {
            In = ParameterLocation.Header,
            Description = "Please provide a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
         });
         
         options.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
            {
               new OpenApiSecurityScheme
               {
                  Reference = new OpenApiReference
                  {
                     Type = ReferenceType.SecurityScheme, 
                     Id = "Bearer"
                  }
               },
               ArraySegment<string>.Empty
            }
         });
         
         
         options.SwaggerDoc("v1", new OpenApiInfo
         {
            Title = AppName,
            Version = "v1",
         });
      });
      
      return services;
   }
   
   public static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app)
   {
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", AppName));
      
      return app;
   }
}