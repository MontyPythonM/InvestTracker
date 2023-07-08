namespace InvestTracker.Users.Core.Dtos;

public class UserDetailsDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public DateTime CreatedAt { get; set; }
    public RoleDto? Role { get; set; }
    public SubscriptionDto? Subscription { get; set; }
}