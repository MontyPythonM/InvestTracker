namespace InvestTracker.Users.Core.Validators;

public static class ResetPasswordKey
{
    public static string Create(Guid userId) => $"{Guid.NewGuid()}={userId}";

    public static string GetUserId(string key)
    {
        if (!IsValid(key))
        {
            throw new FormatException("ResetPasswordKey has invalid format");
        }
        
        return key.Split('=')[1];
    }

    public static string GetRandomId(string key)
    {
        if (!IsValid(key))
        {
            throw new FormatException("ResetPasswordKey has invalid format");
        }
        
        return key.Split('=')[0];
    }

    private static bool IsValid(string key)
    {
        return !string.IsNullOrWhiteSpace(key) && key.ToCharArray().Contains('=') && key.Length.Equals(73);
    }
}