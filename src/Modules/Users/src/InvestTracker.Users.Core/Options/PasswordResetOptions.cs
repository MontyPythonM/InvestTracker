namespace InvestTracker.Users.Core.Options;

public class PasswordResetOptions
{
    public int ExpirationMinutes { get; set; }
    public string RedirectTo { get; set; } = string.Empty;
}