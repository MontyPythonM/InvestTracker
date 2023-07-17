namespace InvestTracker.Users.Core.Dtos;

public class SetRoleDto
{
    public Guid UserId { get; set; }
    public string Role { get; set; } = string.Empty;
}