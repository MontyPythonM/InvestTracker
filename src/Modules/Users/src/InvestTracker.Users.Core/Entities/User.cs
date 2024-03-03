using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.Users.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public FullName FullName { get; set; }
    public Email Email { get; set; }
    public PhoneNumber Phone { get; set; }
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public Role Role { get; set; }
    public Subscription Subscription { get; set; }
    public ResetPassword? ResetPassword { get; set; }
    public RefreshToken? RefreshToken { get; set; }
    public DateTime? LastSuccessfulLogin { get; set; }
}