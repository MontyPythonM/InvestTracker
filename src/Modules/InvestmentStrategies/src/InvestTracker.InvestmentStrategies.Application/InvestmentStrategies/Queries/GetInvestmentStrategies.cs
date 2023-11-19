using InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Dto;
using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.InvestmentStrategies.Application.InvestmentStrategies.Queries;

public record GetInvestmentStrategies() : IQuery<IEnumerable<InvestmentStrategiesDto>>;