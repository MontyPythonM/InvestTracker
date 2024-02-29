namespace InvestTracker.Shared.Abstractions.Authentication;

public class RefreshToken
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiredAt { get; set; }
    public long Expires { get; set; }
}