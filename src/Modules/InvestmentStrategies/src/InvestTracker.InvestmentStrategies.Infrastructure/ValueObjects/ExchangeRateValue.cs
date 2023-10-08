using InvestTracker.InvestmentStrategies.Infrastructure.Exceptions;

namespace InvestTracker.InvestmentStrategies.Infrastructure.ValueObjects;

public sealed record ExchangeRateValue
{
    public decimal Value { get; }
    
    public ExchangeRateValue(decimal value)
    {
        if (value is < 0 or > 1_000_000)
        {
            throw new InvalidExchangeRateValueException(value);
        }

        Value = value;
    }
    
    public static implicit operator ExchangeRateValue(decimal value) => new(value);
    public static implicit operator decimal(ExchangeRateValue value) => value.Value;
}