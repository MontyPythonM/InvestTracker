using System.ComponentModel.DataAnnotations;

namespace InvestTracker.Users.Core.Dtos;

public record SignUpDto
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [MinLength(4), MaxLength(100)]
    public string Password { get; set; } = string.Empty;
    
    [MinLength(3), MaxLength(100)]
    public string FullName { get; set; } = string.Empty;
    
    [Phone]
    public string? Phone { get; set; }
}