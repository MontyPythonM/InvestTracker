using InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;

public record Currency
{
    private static readonly HashSet<string> AvailableCurrencies = new()
    {
        "PLN", "USD", "EUR", "GBP", "JPY", "CHF", "NOK"
    };

    public string Value { get;}

    public Currency(string value)
    {
        if (HasValidFormat(value))
        {
            throw new InvalidCurrencyFormatException(value);
        }

        value = value.ToUpper();
        if (!AvailableCurrencies.Contains(value))
        {
            throw new UnsupportedCurrencyException(value);
        }

        Value = value;
    }

    public static implicit operator Currency(string value) => new(value);
    public static implicit operator string(Currency value) => value.Value;
    
    
    private static bool HasValidFormat(string value) => !string.IsNullOrWhiteSpace(value) || value.Length == 3;
}