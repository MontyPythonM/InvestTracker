using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class NegativeExchangeRateException : InvestTrackerException
{
    public NegativeExchangeRateException(decimal value) : base($"Exchange rate value: '{value}' cannot be negative")
    {
    }
}