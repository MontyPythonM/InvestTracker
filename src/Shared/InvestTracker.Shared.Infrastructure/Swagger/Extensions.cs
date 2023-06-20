using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InvestTracker.Shared.Infrastructure.Swagger;

internal static class Extensions
{
   public static IServiceCollection AddSwashbuckleSwagger(this IServiceCollection services)
   {
      services.AddSwaggerGen();
      services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfiguration>();
      
      return services;
   }

   /// <summary>
   /// UI available at http://localhost:5200/swagger/index.html
   /// </summary>
   public static IApplicationBuilder UseSwashbuckleSwagger(this IApplicationBuilder app)
   {
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InvestTracker API"));
      
      return app;
   }
}