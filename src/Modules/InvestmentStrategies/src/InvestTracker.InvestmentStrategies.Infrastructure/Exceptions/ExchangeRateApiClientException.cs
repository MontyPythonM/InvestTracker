using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Exceptions;

internal sealed class ExchangeRateApiClientException : InvestTrackerException
{
    public ExchangeRateApiClientException(string message) : base(message)
    {
    }
}