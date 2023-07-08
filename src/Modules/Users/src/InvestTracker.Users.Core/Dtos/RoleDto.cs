namespace InvestTracker.Users.Core.Dtos;

public class RoleDto
{
    public string? Value { get; set; }
    public DateTime? GrantedAt { get; set; }
    public Guid? GrantedBy { get; set; }
}