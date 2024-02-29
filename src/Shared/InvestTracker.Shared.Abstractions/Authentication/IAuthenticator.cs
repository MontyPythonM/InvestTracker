namespace InvestTracker.Shared.Abstractions.Authentication;

public interface IAuthenticator
{
    AccessToken CreateAccessToken(string userId, string? role = null, string? subscription = null);
    RefreshToken CreateRefreshToken();
    Guid? GetUserFromAccessToken(string accessToken);
}