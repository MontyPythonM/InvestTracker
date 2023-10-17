using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Commands;

public record CreateInvestmentStrategy(string Title, string Note) : ICommand;