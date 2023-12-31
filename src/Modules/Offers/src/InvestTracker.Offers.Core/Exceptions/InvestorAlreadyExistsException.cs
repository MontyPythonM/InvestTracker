﻿using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Offers.Core.Exceptions;

internal class InvestorAlreadyExistsException : InvestTrackerException
{
    public InvestorAlreadyExistsException(Guid id) : base($"Investor with ID: {id} already exists.")
    {
    }
}