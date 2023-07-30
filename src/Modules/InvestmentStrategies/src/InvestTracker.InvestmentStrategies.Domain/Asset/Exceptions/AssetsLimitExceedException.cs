using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;

internal class AssetsLimitExceedException : InvestTrackerException
{
    public AssetsLimitExceedException(int assetsInPortfolio) : base($"Assets in portfolio limit was exceeded.")
    {
    }
}