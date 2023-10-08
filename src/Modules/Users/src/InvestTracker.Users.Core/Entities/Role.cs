using InvestTracker.Shared.Abstractions.Authorization;

namespace InvestTracker.Users.Core.Entities;

public class Role
{
    public string Value { get; set; } = SystemRole.None;
    public DateTime? GrantedAt { get; set; }
    public Guid? GrantedBy { get; set; }
}