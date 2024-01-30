namespace InvestTracker.Shared.Abstractions.Authorization;

public static class SystemRole
{
    public const string None = "None";
    public const string SystemAdministrator = "SystemAdministrator";
    public const string BusinessAdministrator = "BusinessAdministrator";

    public static readonly IReadOnlySet<string> Roles = new HashSet<string>()
    {
        None, SystemAdministrator, BusinessAdministrator
    };
    
    public static bool IsAdministrator(string? role) => role is SystemAdministrator or BusinessAdministrator;
}

