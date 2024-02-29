using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InvestTracker.Shared.Abstractions.Authentication;
using InvestTracker.Shared.Abstractions.Time;
using InvestTracker.Shared.Abstractions.Types;
using Microsoft.IdentityModel.Tokens;

namespace InvestTracker.Shared.Infrastructure.Authentication;

internal sealed class Authenticator : IAuthenticator
{
    private readonly ITimeProvider _timeProvider;
    private readonly AuthOptions _authOptions;
    private readonly SigningCredentials _signingKey;
    
    public Authenticator(ITimeProvider timeProvider, AuthOptions authOptions)
    {
        _timeProvider = timeProvider;
        _authOptions = authOptions;
        _signingKey = new SigningCredentials(new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_authOptions.IssuerSigningKey)), SecurityAlgorithms.HmacSha256);
    }
    
    public AccessToken CreateAccessToken(string userId, string? role = null, string? subscription = null)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User ID claim (subject) cannot be empty.", nameof(userId));
        }

        var now = _timeProvider.Current();
        var expires = now.AddMinutes(_authOptions.ExpiryMinutes);

        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.UniqueName, userId),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString())
        };
        
        if (!string.IsNullOrWhiteSpace(role))
        {
            jwtClaims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        if (!string.IsNullOrWhiteSpace(subscription))
        {
            jwtClaims.Add(new Claim(CustomClaim.Subscription, subscription));
        }
        
        if (_authOptions.Audiences.Any())
        {
            jwtClaims.AddRange(_authOptions.Audiences.Select(audience => new Claim(JwtRegisteredClaimNames.Aud, audience)));
        }
        
        var jwt = new JwtSecurityToken(
            issuer: _authOptions.Issuer,
            claims: jwtClaims,
            notBefore: now,
            expires: expires,
            signingCredentials: _signingKey
        );
        
        return new AccessToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            ExpiredAt = expires,
            Expires = new DateTimeOffset(expires).ToUnixTimeMilliseconds(),
            UserId = userId,
            Role = role,
            Subscription = subscription,
        };
    }

    public RefreshToken CreateRefreshToken()
    {
        var expiredAt = _timeProvider.Current().AddMinutes(_authOptions.RefreshTokenExpiryMinutes);
        
        return new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            ExpiredAt = expiredAt,
            Expires = new DateTimeOffset(expiredAt).ToUnixTimeMilliseconds()
        };
    }

    public Guid? GetUserFromAccessToken(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.IssuerSigningKey)),
            ValidIssuer = _authOptions.ValidIssuer,
            ValidAudiences = _authOptions.ValidAudiences,
            ValidateAudience = _authOptions.ValidateAudience,
            ValidateIssuer = _authOptions.ValidateIssuer,
            ValidateLifetime = _authOptions.ValidateLifetime,
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);

        return principal.Identity?.Name?.ToNullableGuid();
    }
}