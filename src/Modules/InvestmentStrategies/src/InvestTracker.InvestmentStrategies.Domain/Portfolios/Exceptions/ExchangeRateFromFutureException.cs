using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class ExchangeRateFromFutureException : InvestTrackerException
{
    public ExchangeRateFromFutureException(DateOnly date) : base($"The exchange rate date: '{date}' cannot be in the future")
    {
    }
}