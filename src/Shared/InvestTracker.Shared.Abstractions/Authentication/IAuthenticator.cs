using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.Shared.Abstractions.Authentication;

public interface IAuthenticator
{
    AccessTokenDto CreateAccessToken(Guid userId, Email email, Role? role = null, Subscription? subscription = null);
    RefreshTokenDto CreateRefreshToken();
}