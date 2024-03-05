namespace InvestTracker.Users.Core.Options;

public class PasswordResetOptions
{
    public int ExpirationMinutes { get; set; } = 10;
    public string RedirectTo { get; set; } = string.Empty;
    public bool UseResetPasswordPolicyPolicy { get; set; } = false;
    public int ResetPasswordPolicyMultiplierMinutes { get; set; } = 3;
}