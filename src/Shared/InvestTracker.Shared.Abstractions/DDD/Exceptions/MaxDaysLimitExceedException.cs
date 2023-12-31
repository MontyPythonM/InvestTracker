﻿using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.Exceptions;

public sealed class MaxDaysLimitExceedException : InvestTrackerException
{
    public MaxDaysLimitExceedException(uint limit) 
        : base($"Date range's day limit ({limit} days) has been exceeded")
    {
    }
}