namespace InvestTracker.Shared.Abstractions.Authentication;

public class AccessTokenDto
{
    public string Token { get; set; } = string.Empty;
    public long Expires { get; set; }
    public DateTime ExpiredAt { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string? Role { get; set; }
    public string? Subscription { get; set; }
    public string Email { get; set; } = string.Empty;
}