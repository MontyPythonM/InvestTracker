using InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;

public record Amount
{
    public decimal Value { get; }
    
    public Amount(decimal value)
    {
        if (value is < 0 or > 1_000_000_000)
        {
            throw new InvalidAmountException(value);
        }

        Value = value;
    }
    
    public static implicit operator Amount(decimal value) => new(value);
    public static implicit operator decimal(Amount value) => value.Value;
}