using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;

public class FinancialAssetNotFoundException : InvestTrackerException
{
    public FinancialAssetNotFoundException(FinancialAssetId id) : base($"Financial asset with ID: {id} not found")
    {
    }
}