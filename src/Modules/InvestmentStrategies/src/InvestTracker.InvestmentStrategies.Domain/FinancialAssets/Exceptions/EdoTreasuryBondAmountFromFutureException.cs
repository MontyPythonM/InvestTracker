using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Exceptions;

internal sealed class EdoTreasuryBondAmountFromFutureException : InvestTrackerException
{
    public EdoTreasuryBondAmountFromFutureException() : base("Cannot calculate amount of EDO asset for date in future.")
    {
    }
}