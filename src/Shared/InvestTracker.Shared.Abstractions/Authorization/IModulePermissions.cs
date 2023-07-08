namespace InvestTracker.Shared.Abstractions.Authorization;

public interface IModulePermissions
{
    public List<Permission> Permissions { get; set; }
}

