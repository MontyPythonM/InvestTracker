using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.Exceptions;

internal sealed class InvalidDateRangeException : InvestTrackerException
{
    public InvalidDateRangeException() : base($"'{nameof(DateRange.From)}' cannot be greater than '{nameof(DateRange.To)}'.")
    {
    }
}