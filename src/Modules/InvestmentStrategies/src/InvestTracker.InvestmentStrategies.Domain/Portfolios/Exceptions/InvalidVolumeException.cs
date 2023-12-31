﻿using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;

internal sealed class InvalidVolumeException : InvestTrackerException
{
    public InvalidVolumeException(int value) : base($"Volume with value: {value} has invalid format.")
    {
    }
}