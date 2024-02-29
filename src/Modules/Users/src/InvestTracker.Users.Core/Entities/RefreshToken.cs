namespace InvestTracker.Users.Core.Entities;

public class RefreshToken
{
    public string Token { get; set; }
    public DateTime ExpiredAt { get; set; }
}