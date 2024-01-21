using System.ComponentModel.DataAnnotations;

namespace InvestTracker.Users.Core.Dtos;

public class DeleteAccountDto
{
    [Required]
    public string Password { get; set; }
}