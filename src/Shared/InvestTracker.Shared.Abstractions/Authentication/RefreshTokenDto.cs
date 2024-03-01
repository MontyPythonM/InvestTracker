namespace InvestTracker.Shared.Abstractions.Authentication;

public record RefreshTokenDto(string Token, DateTime ExpiredAt);