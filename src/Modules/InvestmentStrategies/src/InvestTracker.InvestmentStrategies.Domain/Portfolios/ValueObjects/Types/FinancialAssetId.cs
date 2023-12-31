﻿namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;

public readonly record struct FinancialAssetId(Guid Value)
{
    public static implicit operator Guid(FinancialAssetId id) => id.Value;
    public static implicit operator FinancialAssetId(Guid id) => new(id);
}