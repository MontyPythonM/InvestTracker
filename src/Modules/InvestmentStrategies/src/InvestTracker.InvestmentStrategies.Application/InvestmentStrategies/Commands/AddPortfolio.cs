using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Commands;

public record AddPortfolio(InvestmentStrategyId StrategyId, Title Title, Note Note, Description Description) : ICommand;