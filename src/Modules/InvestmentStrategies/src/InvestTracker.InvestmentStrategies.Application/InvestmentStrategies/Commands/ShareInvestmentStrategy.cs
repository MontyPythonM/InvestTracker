using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Commands;

public record ShareInvestmentStrategy(InvestmentStrategyId InvestmentStrategyId, StakeholderId ShareWith) : ICommand;