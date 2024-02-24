namespace InvestTracker.Users.Core.Entities;

public class ResetPassword
{
    public string? Key { get; set; }
    public DateTime? InvokeAt { get; set; }
    public DateTime? ExpiredAt { get; set; }
}