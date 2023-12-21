using InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;
using InvestTracker.Shared.Abstractions.Types;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;

public sealed record ExchangeRate
{
    public Currency From { get; }
    public Currency To { get; }
    public DateOnly Date { get; }
    public decimal Value { get; }

    public ExchangeRate(Currency from, Currency to, DateOnly date, decimal value)
    {
        if (date > DateTime.Now.ToDateOnly())
        {
            throw new ExchangeRateFromFutureException(date);
        }

        if (value <= 0)
        {
            throw new NegativeExchangeRateException(value);
        }

        From = from;
        To = to;
        Date = date;
        Value = value;
    }
}