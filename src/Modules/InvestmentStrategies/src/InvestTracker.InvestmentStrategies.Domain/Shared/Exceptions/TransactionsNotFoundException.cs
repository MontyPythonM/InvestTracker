﻿using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Shared.Exceptions;

public class TransactionsNotFoundException : InvestTrackerException
{
    public TransactionsNotFoundException(TransactionId id) : base($"Transaction with ID: '{id.Value}' not found.")
    {
    }
}