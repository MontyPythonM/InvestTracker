using System.ComponentModel.DataAnnotations;

namespace InvestTracker.Users.Core.Dtos;

public class AuthTokenDto
{
    [Required]
    public string ExpiredAccessToken { get; set; } = string.Empty;
    
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}