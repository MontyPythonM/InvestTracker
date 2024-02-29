using System.Reflection;
using InvestTracker.Shared.Abstractions.Authorization;

namespace InvestTracker.Users.Api.Permissions;

internal sealed class UsersPermissionMatrix : IModulePermissionMatrix
{
    public string GetModuleName() => Assembly.GetExecutingAssembly().GetName().Name!;
    
    public ISet<Permission> Permissions { get; } = new HashSet<Permission>()
    {
        new(SystemRole.SystemAdministrator, UsersPermission.GetUsers.ToString()),
        new(SystemRole.SystemAdministrator, UsersPermission.GetUserDetails.ToString()),
        new(SystemRole.SystemAdministrator, UsersPermission.SetRole.ToString()),
        new(SystemRole.SystemAdministrator, UsersPermission.RemoveRole.ToString()),
        new(SystemRole.SystemAdministrator, UsersPermission.SetSubscription.ToString()),
        new(SystemRole.SystemAdministrator, UsersPermission.ActivateUserAccount.ToString()),
        new(SystemRole.SystemAdministrator, UsersPermission.DeactivateUserAccount.ToString()),
        new(SystemRole.SystemAdministrator, UsersPermission.RevokeToken.ToString()),

        new(SystemRole.BusinessAdministrator, UsersPermission.GetUsers.ToString()),
        new(SystemRole.BusinessAdministrator, UsersPermission.GetUserDetails.ToString()),
        new(SystemRole.BusinessAdministrator, UsersPermission.SetSubscription.ToString()),
        new(SystemRole.BusinessAdministrator, UsersPermission.RevokeToken.ToString()),
    };
}