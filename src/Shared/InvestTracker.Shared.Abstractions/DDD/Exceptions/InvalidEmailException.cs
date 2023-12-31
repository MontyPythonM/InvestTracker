﻿using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.Exceptions;

internal sealed class InvalidEmailException : InvestTrackerException
{
    public InvalidEmailException(string email) : base($"Email '{email}' has invalid format.")
    {
    }
}