using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class DateOutOfInvestmentPeriodRangeException : InvestTrackerException
{
    public DateOutOfInvestmentPeriodRangeException(DateOnly date) : base($"Date: '{date}' is out of investment period range")
    {
    }
}