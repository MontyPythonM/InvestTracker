﻿using InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;

public record Margin
{
    public decimal Value { get; }
    
    public Margin(decimal value)
    {
        if (value is < 0 or > 100)
        {
            throw new InvalidMarginException(value);
        }

        Value = value;
    }
    
    public static implicit operator Margin(decimal value) => new(value);
    public static implicit operator decimal(Margin value) => value.Value; 
}