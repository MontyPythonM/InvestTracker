using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;

internal class InvalidVolumeException : InvestTrackerException
{
    public InvalidVolumeException(int value) : base($"Volume with value: {value} has invalid format.")
    {
    }
}