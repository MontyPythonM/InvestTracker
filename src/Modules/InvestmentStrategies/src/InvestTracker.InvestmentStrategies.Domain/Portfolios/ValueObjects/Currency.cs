using InvestTracker.InvestmentStrategies.Domain.Portfolios.Consts;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;

public record Currency
{
    public string Value { get; }

    public Currency(string value)
    {
        if (!HasValidFormat(value))
        {
            throw new InvalidCurrencyFormatException(value);
        }
        
        value = value.ToUpper();
        if (!Currencies.IsSupported(value))
        {
            throw new UnsupportedCurrencyException(value);
        }

        Value = value;
    }

    public static implicit operator Currency(string value) => new(value);
    public static implicit operator string(Currency value) => value.Value;
    
    private static bool HasValidFormat(string value) => !string.IsNullOrWhiteSpace(value) && value.Length == 3;
}