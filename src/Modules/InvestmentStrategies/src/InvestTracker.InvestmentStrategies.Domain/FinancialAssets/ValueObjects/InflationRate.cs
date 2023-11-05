using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects;

public sealed record InflationRate
{
    public decimal Value { get; }
    public Currency Currency { get; }
    public int Year { get; }
    public int Month { get; }

    public InflationRate(decimal value, Currency currency, int year, int month)
    {
        if (value is < -100 or > 100)
        {
            throw new InvalidInflationRateException(value);
        }

        if (!IsValidYear(year) || !IsValidMonth(month))
        {
            throw new InvalidInflationRateException();
        }

        Value = value;
        Currency = currency;
        Year = year;
        Month = month;
    }

    private static bool IsValidYear(int year) => year > 1900;
    private static bool IsValidMonth(int month) => month >= 1 && month <= 12;
}