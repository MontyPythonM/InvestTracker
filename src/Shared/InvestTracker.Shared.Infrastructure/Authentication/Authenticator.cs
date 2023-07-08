using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InvestTracker.Shared.Abstractions.Authentication;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.IdentityModel.Tokens;

namespace InvestTracker.Shared.Infrastructure.Authentication;

internal sealed class Authenticator : IAuthenticator
{
    private readonly ITime _time;
    private readonly AuthOptions _authOptions;
    private readonly SigningCredentials _signingKey;
    
    public Authenticator(ITime time, AuthOptions authOptions)
    {
        _time = time;
        _authOptions = authOptions;
        _signingKey = new SigningCredentials(new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_authOptions.IssuerSigningKey)), SecurityAlgorithms.HmacSha256);
    }
    
    public JsonWebToken CreateToken(string userId, string? role = null, string? subscription = null)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User ID claim (subject) cannot be empty.", nameof(userId));
        }

        var now = _time.Current();
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
        
        return new JsonWebToken
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
            ExpiredAt = expires,
            Expires = new DateTimeOffset(expires).ToUnixTimeMilliseconds(),
            UserId = userId,
            Role = role,
            Subscription = subscription,
        };
    }
}