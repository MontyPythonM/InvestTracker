using System.Reflection;
using InvestTracker.Shared.Abstractions.Authorization;

namespace InvestTracker.Users.Api.Permissions;

internal sealed class UsersPermissionMatrix : IModulePermissionMatrix
{
    public string GetModuleName() => Assembly.GetExecutingAssembly().GetName().Name!;
    
    public List<Permission> Permissions { get; set; } = new()
    {
        new Permission(SystemRole.SystemAdministrator, UserPermission.GetUsers.ToString()),
        new Permission(SystemRole.SystemAdministrator, UserPermission.GetUserDetails.ToString()),
        
        new Permission(SystemRole.BusinessAdministrator, UserPermission.GetUsers.ToString())
    };
}