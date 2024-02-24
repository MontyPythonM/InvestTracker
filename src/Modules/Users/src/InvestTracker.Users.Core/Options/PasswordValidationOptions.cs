namespace InvestTracker.Users.Core.Options;

public class PasswordValidationOptions
{
    public string SpecialCharacters { get; set; }
    public int MinimumLength { get; set; }
    public int MaximumLength { get; set; }
}