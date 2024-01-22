using System.ComponentModel.DataAnnotations;

namespace InvestTracker.Users.Core.Dtos;

public record SignUpDto
{
    [Required, EmailAddress]
    public string Email { get; init; } = string.Empty;
    
    [Required, MinLength(4), MaxLength(100)]
    public string Password { get; init; } = string.Empty;
    
    [Required, MinLength(3), MaxLength(100)]
    public string FullName { get; init; } = string.Empty;
    
    [Phone]
    public string Phone { get; init; } = string.Empty;
}