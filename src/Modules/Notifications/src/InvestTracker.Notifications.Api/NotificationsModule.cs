using InvestTracker.Shared.Abstractions.Modules;

namespace InvestTracker.Notifications.Api;

internal class NotificationsModule : IModule
{
    public const string BasePath = "notifications-module";
    
    public string Title => "Notifications Module";
    public string Path => BasePath;
    public string SwaggerGroup => BasePath;
    public string Version => "v1";
}