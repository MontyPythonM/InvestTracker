using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;

internal class InvalidVolumeException : InvestTrackerException
{
    public InvalidVolumeException(int volume) : base($"Volume value: '{volume}' is less than zero.")
    {
    }
}