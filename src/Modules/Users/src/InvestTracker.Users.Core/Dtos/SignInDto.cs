using System.ComponentModel.DataAnnotations;

namespace InvestTracker.Users.Core.Dtos;

public record SignInDto([Required, EmailAddress] string Email, [Required] string Password);