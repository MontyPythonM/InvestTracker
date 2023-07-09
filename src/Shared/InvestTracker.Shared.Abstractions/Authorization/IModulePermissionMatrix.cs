using System.Reflection;

namespace InvestTracker.Shared.Abstractions.Authorization;

public interface IModulePermissionMatrix
{
    public string GetModuleName();
    public ISet<Permission> Permissions { get; }
}