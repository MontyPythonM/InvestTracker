using InvestTracker.Shared.Abstractions.DDD.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.ValueObjects;

public record FullName
{
    public string Value { get; }

    public FullName(string value)
    {
        if (!IsValidFullName(value))
        {
            throw new InvalidFullNameException(value);
        }

        Value = value;
    }
    
    public static implicit operator string(FullName fullName) => fullName.Value;
    public static implicit operator FullName(string fullName) => new(fullName);

    private static bool IsValidFullName(string value) => !string.IsNullOrWhiteSpace(value) && value.Length < 100;
}