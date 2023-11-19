using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class InsufficientAssetVolumeException : InvestTrackerException
{
    public InsufficientAssetVolumeException(Guid id) : base($"Insufficient volume for asset with ID: '{id}")
    {
    }
}