using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Commands;

public record RemoveCashTransaction(PortfolioId PortfolioId, FinancialAssetId FinancialAssetId, TransactionId TransactionId) : ICommand;