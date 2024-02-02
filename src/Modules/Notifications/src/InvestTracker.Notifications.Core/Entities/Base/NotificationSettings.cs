namespace InvestTracker.Notifications.Core.Entities.Base;

public abstract class NotificationSettings
{
    public Guid Id { get; set; }
    public bool EnableNotifications { get; set; } = true;
    public bool EnableEmails { get; set; } = false;
    public bool AdministratorsActivity { get; set; } = false;
    public bool InvestmentStrategiesActivity { get; set; } = true;
    public bool PortfoliosActivity { get; set; } = true;
    public bool AssetActivity { get; set; } = true;
    public bool ExistingCollaborationsActivity { get; set; } = true;
    public bool NewCollaborationsActivity { get; set; } = true;
}