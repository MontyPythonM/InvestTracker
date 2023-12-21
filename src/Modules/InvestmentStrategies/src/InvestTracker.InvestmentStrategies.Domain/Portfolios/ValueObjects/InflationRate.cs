using InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;

public sealed record InflationRate
{
    public decimal Value { get; }
    public Currency Currency { get; }
    public MonthlyDate MonthlyDate { get; }

    public InflationRate(decimal value, Currency currency, int year, int month)
    {
        if (value is < -100 or > 100)
        {
            throw new InvalidInflationRateException(value);
        }

        Value = value;
        Currency = currency;
        MonthlyDate = new MonthlyDate(year, month);
    }
}