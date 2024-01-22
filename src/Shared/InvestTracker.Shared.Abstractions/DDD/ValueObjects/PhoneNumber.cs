using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using InvestTracker.Shared.Abstractions.DDD.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.ValueObjects;

public record PhoneNumber
{
    public string Value { get; }
    
    public PhoneNumber(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Value = string.Empty;
            return;
        }
        
        var formattedValue = FormatPhoneNumber(value);
        
        if (!IsValidPhoneNumber(formattedValue))
        {
            throw new InvalidPhoneNumberException(formattedValue);
        }

        Value = formattedValue;
    }
    
    public static implicit operator string(PhoneNumber email) => email.Value;
    public static implicit operator PhoneNumber(string email) => new(email);
    
    private static bool IsValidPhoneNumber(string value) 
        => !string.IsNullOrEmpty(value) && value.Length < 20 && new PhoneAttribute().IsValid(value); 
    
    private static string FormatPhoneNumber(string input)
    {
        const string phoneNumberCharsRegex = @"[^0-9]";
        return Regex.Replace(input, phoneNumberCharsRegex, string.Empty);
    }
}