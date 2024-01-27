using System.ComponentModel.DataAnnotations;
using InvestTracker.Shared.Abstractions.DDD.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.ValueObjects;

public record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (!IsValidEmail(value))
        {
            throw new InvalidEmailException(value);
        }

        Value = value.ToLowerInvariant();
    }
    
    public static implicit operator string(Email email) => email.Value;
    public static implicit operator Email(string email) => new(email);
    
    private static bool IsValidEmail(string value) => 
        !string.IsNullOrWhiteSpace(value) && value.Length < 100 && new EmailAddressAttribute().IsValid(value);
}