namespace InvestTracker.Shared.Abstractions.Authentication;

public interface IAuthenticator
{
    JsonWebToken CreateToken(string userId, string? role = null, string? subscription = null);
}