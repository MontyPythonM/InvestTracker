﻿namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

public readonly record struct PortfolioId(Guid Value)
{
    public static implicit operator Guid(PortfolioId id) => id.Value;
    public static implicit operator PortfolioId(Guid id) => new(id);
}