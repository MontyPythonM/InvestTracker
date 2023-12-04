using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Commands;

public record DeductCashTransaction(FinancialAssetId FinancialAssetId, PortfolioId PortfolioId, Amount Amount, 
    DateTime TransactionDate, Note Note) : ICommand;