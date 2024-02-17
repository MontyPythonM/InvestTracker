namespace InvestTracker.Notifications.Api.Permissions;

public enum NotificationsPermission
{
    SendNotification,
    GetRecipientsGroups,
    SendNotificationToGroup,
    SendForceNotification,
    GetGlobalSettings,
    SetGlobalSettings,
    GetPersonalSettings,
    SetPersonalSettings,
    GetReceivers,
    SendEmail
}