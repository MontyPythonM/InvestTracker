using InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;

public record Broker
{
    public string Value { get; }

    public Broker(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 100)
        {
            throw new InvalidBrokerException(value);
        }

        Value = value;
    }
    
    public static implicit operator Broker(string value) => new(value);
    public static implicit operator string(Broker value) => value.Value;
}