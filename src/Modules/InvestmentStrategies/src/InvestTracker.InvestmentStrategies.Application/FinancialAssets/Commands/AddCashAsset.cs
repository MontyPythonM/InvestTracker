using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.InvestmentStrategies.Application.FinancialAssets.Commands;

public record AddCashAsset(Guid PortfolioId, string Currency, string Note, decimal? InitialAmount) : ICommand;