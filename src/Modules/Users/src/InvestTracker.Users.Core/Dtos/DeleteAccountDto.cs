using System.ComponentModel.DataAnnotations;

namespace InvestTracker.Users.Core.Dtos;

public class DeleteAccountDto
{
    public string Password { get; set; }
}