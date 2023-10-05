using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Exceptions;

internal sealed class InvalidExchangeRateValueException : InvestTrackerException
{
    public InvalidExchangeRateValueException(decimal value) : base($"Exchange rate value: '{value}' is invalid.")
    {
    }
}