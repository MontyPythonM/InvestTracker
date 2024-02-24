using System.ComponentModel.DataAnnotations;

namespace InvestTracker.Users.Core.Dtos;

public class ResetPasswordDto
{
    [Required]
    public string ResetPasswordKey { get; set; }
    
    [Required]
    public string NewPassword { get; set; }
    
    [Required]
    public string ConfirmNewPassword { get; set; }
}