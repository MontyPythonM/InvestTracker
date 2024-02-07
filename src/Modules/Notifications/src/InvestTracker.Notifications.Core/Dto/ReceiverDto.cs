namespace InvestTracker.Notifications.Core.Dto;

public class ReceiverDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Subscription { get; set; }
    public string Role { get; set; }
    public PersonalSettingsDto PersonalSettings { get; set; }
}