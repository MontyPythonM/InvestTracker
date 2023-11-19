using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class PlannedRedemptionTooEarlyException : InvestTrackerException
{
    public PlannedRedemptionTooEarlyException(DateOnly redemptionDate) 
        : base($"Asset cannot be closed due to too early date. Redemption will take place on {redemptionDate}.")
    {
    }
}