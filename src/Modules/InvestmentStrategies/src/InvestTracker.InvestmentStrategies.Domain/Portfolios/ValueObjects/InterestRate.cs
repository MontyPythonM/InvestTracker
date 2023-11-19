using InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;

public sealed record InterestRate
{
    public decimal Value { get; }
    
    public InterestRate(decimal percentageValue)
    {
        if (percentageValue is < 0 or > 100)
        {
            throw new InvalidInterestRateException(percentageValue);
        }

        Value = percentageValue;
    }

    public static implicit operator InterestRate(decimal value) => new(value);
    public static implicit operator decimal(InterestRate value) => value.Value;
}