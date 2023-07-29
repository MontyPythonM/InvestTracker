using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;

internal class InvalidBrokerException : InvestTrackerException
{
    public InvalidBrokerException(string broker) : base($"Broker: {broker} is invalid.")
    {
    }
}