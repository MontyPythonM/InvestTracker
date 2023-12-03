using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;

public record ChartValue(DateOnly Date, Amount Amount);