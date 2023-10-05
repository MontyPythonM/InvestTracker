using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects;

public record Description
{
    public string Value { get; }
    
    public Description(string value)
    {
        if (value.Length > 2000)
        {
            throw new InvalidDescriptionException();
        }

        Value = value;
    }
    
    public static implicit operator string(Description description) => description.Value;
    public static implicit operator Description(string description) => new(description);
}