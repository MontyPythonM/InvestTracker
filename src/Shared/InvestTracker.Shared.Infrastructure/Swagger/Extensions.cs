using InvestTracker.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace InvestTracker.Shared.Infrastructure.Swagger;

internal static class Extensions
{
   private const string AppName = "InvestTracker API";
   
   public static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services, IList<IModule> modules)
   {
      services.AddSwaggerGen(options =>
      {
         options.EnableAnnotations();

         foreach (var module in modules)
         {
            options.SwaggerDoc(module.SwaggerGroup, new OpenApiInfo { Title = $"{AppName} - {module.Title}", Version = module.Version });
         }

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
      });
      
      return services;
   }

   public static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app, IList<IModule> modules)
   {
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
         foreach (var module in modules)
         {
            c.SwaggerEndpoint($"/swagger/{module.SwaggerGroup}/swagger.json", module.SwaggerGroup);
         }
      });

      return app;
   }
}