using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Commands;

public record AddPortfolio(InvestmentStrategyId StrategyId, string Title, string Note, string Description) : ICommand;