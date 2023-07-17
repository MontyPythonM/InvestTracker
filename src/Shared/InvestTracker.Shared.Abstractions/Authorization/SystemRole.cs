namespace InvestTracker.Shared.Abstractions.Authorization;

public static class SystemRole
{
    public const string SystemAdministrator = "SystemAdministrator";
    public const string BusinessAdministrator = "BusinessAdministrator";

    public static readonly IReadOnlySet<string> Roles = new HashSet<string>()
    {
        SystemAdministrator, BusinessAdministrator
    };
}

