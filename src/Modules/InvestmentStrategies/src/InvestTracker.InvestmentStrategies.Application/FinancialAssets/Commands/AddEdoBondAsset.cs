using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Application.FinancialAssets.Commands;

public record AddEdoBondAsset(PortfolioId PortfolioId, Volume Volume, DateTime PurchaseDate, 
    InterestRate FirstYearInterestRate, Margin Margin, Note Note) : ICommand;