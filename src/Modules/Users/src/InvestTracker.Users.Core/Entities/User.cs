namespace InvestTracker.Users.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public Role Role { get; set; }
    public Subscription Subscription { get; set; }
    public string? ConfirmationKey { get; set; }
}