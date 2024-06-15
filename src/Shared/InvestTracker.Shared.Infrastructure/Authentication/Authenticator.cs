using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InvestTracker.Shared.Abstractions.Authentication;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
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
    
    public AccessTokenDto CreateAccessToken(Guid userId, Email email, Role? role = null, Subscription? subscription = null)
    {
        var now = _timeProvider.Current();
        var expires = now.AddMinutes(_authOptions.ExpiryMinutes);

        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString())
        };
        
        if (!string.IsNullOrWhiteSpace(role?.Value))
        {
            jwtClaims.Add(new Claim(CustomClaim.Role, role));
        }
        
        if (!string.IsNullOrWhiteSpace(subscription?.Value))
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
        
        return new AccessTokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            ExpiredAt = expires,
            Expires = new DateTimeOffset(expires).ToUnixTimeMilliseconds(),
            UserId = userId.ToString(),
            Role = role?.Value,
            Subscription = subscription?.Value,
            Email = email.Value
        };
    }

    public RefreshTokenDto CreateRefreshToken()
    {
        var expiredAt = _timeProvider.Current().AddMinutes(_authOptions.RefreshTokenExpiryMinutes);

        return new RefreshTokenDto(Guid.NewGuid().ToString(), expiredAt);
    }
}