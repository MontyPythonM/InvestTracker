namespace InvestTracker.Notifications.Core.Dto;

public class PersonalSettingsDto
{
    public bool EnableNotifications { get; set; }
    public bool EnableEmails { get; set; }
    public bool InvestmentStrategiesActivity { get; set; }
    public bool PortfoliosActivity { get; set; }
    public bool AssetActivity { get; set; }
    public bool ExistingCollaborationsActivity { get; set; }
    public bool NewCollaborationsActivity { get; set; }
}