using InvestTracker.Shared.Abstractions.DDD.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.ValueObjects;

public record Title
{
    public string Value { get; }

    public Title(string value)
    {
        if (!IsValidTitle(value))
        {
            throw new InvalidTitleException(value);
        }

        Value = value;
    }
    
    public static implicit operator string(Title title) => title.Value;
    public static implicit operator Title(string title) => new(title);

    private static bool IsValidTitle(string value) => !string.IsNullOrWhiteSpace(value) || value.Length < 100;
}