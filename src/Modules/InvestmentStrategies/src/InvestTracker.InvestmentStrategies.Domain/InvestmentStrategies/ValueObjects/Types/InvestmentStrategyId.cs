﻿namespace InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;

public record InvestmentStrategyId(Guid Value)
{
    public static implicit operator Guid(InvestmentStrategyId id) => id.Value;
    public static implicit operator InvestmentStrategyId(Guid id) => new(id);
}