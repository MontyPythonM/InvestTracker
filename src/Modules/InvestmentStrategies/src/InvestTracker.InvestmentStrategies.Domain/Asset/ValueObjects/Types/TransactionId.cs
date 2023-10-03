﻿namespace InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects.Types;

public record TransactionId(Guid Value)
{
    public static implicit operator Guid(TransactionId id) => id.Value;
    public static implicit operator TransactionId(Guid id) => new(id);
}