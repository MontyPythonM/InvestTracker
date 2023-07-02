namespace InvestTracker.Users.Core.Entities;

public class Role
{
    public string? Value { get; set; }
    public DateTime? GrantedAt { get; set; }
    public Guid? GrantedBy { get; set; }
}