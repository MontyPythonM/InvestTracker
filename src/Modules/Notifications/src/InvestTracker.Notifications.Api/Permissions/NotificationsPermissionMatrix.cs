using System.Reflection;
using InvestTracker.Shared.Abstractions.Authorization;

namespace InvestTracker.Notifications.Api.Permissions;

internal sealed class NotificationsPermissionMatrix : IModulePermissionMatrix
{
    public string GetModuleName() => Assembly.GetExecutingAssembly().GetName().Name!;
    
    public ISet<Permission> Permissions { get; } = new HashSet<Permission>()
    {
        new(SystemRole.SystemAdministrator, NotificationsPermission.SendNotification.ToString()),
        new(SystemRole.SystemAdministrator, NotificationsPermission.GetRecipientsGroups.ToString()),
    };
}