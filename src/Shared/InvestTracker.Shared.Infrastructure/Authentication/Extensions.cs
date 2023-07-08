using System.Text;
using InvestTracker.Shared.Abstractions.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace InvestTracker.Shared.Infrastructure.Authentication;

internal static class Extensions
{
    public static IServiceCollection AddAppAuthentication(this IServiceCollection services)
    {
        services.AddSingleton<IAuthenticator, Authenticator>();

        var authOptions = services.GetOptions<AuthOptions>(AuthOptions.SectionName);
        services.AddSingleton(authOptions);
        
        if (string.IsNullOrWhiteSpace(authOptions.IssuerSigningKey))
        {
            throw new ArgumentException("Missing issuer signing key.", nameof(authOptions.IssuerSigningKey));
        }

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = authOptions.RequireHttpsMetadata;
                config.SaveToken = authOptions.SaveToken;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.IssuerSigningKey)),
                    ValidIssuer = authOptions.ValidIssuer,
                    ValidAudiences = authOptions.ValidAudiences,
                    ValidateAudience = authOptions.ValidateAudience,
                    ValidateIssuer = authOptions.ValidateIssuer,
                    ValidateLifetime = authOptions.ValidateLifetime,
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        return services;
    }
}