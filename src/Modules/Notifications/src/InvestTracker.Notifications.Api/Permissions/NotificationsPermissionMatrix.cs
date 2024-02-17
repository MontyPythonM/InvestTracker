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
        new(SystemRole.SystemAdministrator, NotificationsPermission.SendNotificationToGroup.ToString()),
        new(SystemRole.SystemAdministrator, NotificationsPermission.SendForceNotification.ToString()),
        new(SystemRole.SystemAdministrator, NotificationsPermission.GetGlobalSettings.ToString()),
        new(SystemRole.SystemAdministrator, NotificationsPermission.SetGlobalSettings.ToString()),
        new(SystemRole.SystemAdministrator, NotificationsPermission.GetPersonalSettings.ToString()),
        new(SystemRole.SystemAdministrator, NotificationsPermission.SetPersonalSettings.ToString()),
        new(SystemRole.SystemAdministrator, NotificationsPermission.GetReceivers.ToString()),
        new(SystemRole.SystemAdministrator, NotificationsPermission.SendEmail.ToString()),
        
        new(SystemRole.BusinessAdministrator, NotificationsPermission.GetPersonalSettings.ToString()),
        new(SystemRole.BusinessAdministrator, NotificationsPermission.SetPersonalSettings.ToString()),
        
        new(SystemSubscription.StandardInvestor, NotificationsPermission.GetPersonalSettings.ToString()),
        new(SystemSubscription.StandardInvestor, NotificationsPermission.SetPersonalSettings.ToString()),
        
        new(SystemSubscription.ProfessionalInvestor, NotificationsPermission.GetPersonalSettings.ToString()),
        new(SystemSubscription.ProfessionalInvestor, NotificationsPermission.SetPersonalSettings.ToString()),
        
        new(SystemSubscription.Advisor, NotificationsPermission.GetPersonalSettings.ToString()),
        new(SystemSubscription.Advisor, NotificationsPermission.SetPersonalSettings.ToString()),
    };
}