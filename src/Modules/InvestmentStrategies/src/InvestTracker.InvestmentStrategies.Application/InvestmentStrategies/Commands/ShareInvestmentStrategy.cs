using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Commands;

public record ShareInvestmentStrategy(Guid InvestmentStrategyId, Guid ShareWith) : ICommand;