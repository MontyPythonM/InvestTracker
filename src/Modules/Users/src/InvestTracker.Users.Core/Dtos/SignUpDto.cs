using System.ComponentModel.DataAnnotations;

namespace InvestTracker.Users.Core.Dtos;

public record SignUpDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
    
    [Required]
    public string FullName { get; set; } = string.Empty;
    
    [Phone]
    public string Phone { get; set; } = string.Empty;
}