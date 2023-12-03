using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.SharedExceptions;

public class TransactionsNotExistsException : InvestTrackerException
{
    public TransactionsNotExistsException(FinancialAssetId assetId) : base($"No transactions on financial assets with identifier: {assetId}")
    {
    }
}