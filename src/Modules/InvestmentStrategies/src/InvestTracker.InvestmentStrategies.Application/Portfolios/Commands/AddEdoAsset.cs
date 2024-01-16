using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Commands;

public record AddEdoAsset(PortfolioId PortfolioId, Volume Volume, DateOnly PurchaseDate, 
    InterestRate FirstYearInterestRate, Margin Margin, Note Note) : ICommand;