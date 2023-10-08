using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Exceptions;

internal sealed class ExchangeRateAlreadySeededException : InvestTrackerException
{
    public ExchangeRateAlreadySeededException() : base("Cannot seed exchange rates, because data already exists")
    {
    }
}