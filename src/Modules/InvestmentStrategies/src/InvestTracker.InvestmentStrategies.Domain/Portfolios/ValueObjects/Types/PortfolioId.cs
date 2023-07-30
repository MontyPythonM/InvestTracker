﻿namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;

public record PortfolioId(Guid Value)
{
    public static implicit operator Guid(PortfolioId id) => id.Value;
    public static implicit operator PortfolioId?(Guid id) => id.Equals(Guid.Empty) ? null : new PortfolioId(id);
}