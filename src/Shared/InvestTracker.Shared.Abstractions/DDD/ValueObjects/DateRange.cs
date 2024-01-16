using InvestTracker.Shared.Abstractions.DDD.Exceptions;
using InvestTracker.Shared.Abstractions.Types;

namespace InvestTracker.Shared.Abstractions.DDD.ValueObjects;

public sealed record DateRange
{
    public DateOnly From { get; }
    public DateOnly To { get; }

    public DateRange(DateOnly from, DateOnly to)
    {
        if (from > to)
        {
            throw new InvalidDateRangeException();
        }
        
        From = from;
        To = to;
    }

    public int GetDaysNumber() => CalculateDays(From, To);
    
    public bool IsDaysLimitExceed(uint limit) => CalculateDays(From, To) > limit;

    public override string ToString() => $"Date range from {From} to {To}";
    
    public IEnumerable<DateOnly> GetDates() 
        => Enumerable.Range(0, CalculateDays(From, To)).Select(offset => From.AddDays(offset));

    public IEnumerable<DateRange> DividePerDays(uint daysLimit)
    {
        if (!IsDaysLimitExceed(daysLimit))
        {
            return new List<DateRange> { new(From, To) };
        }
        
        var currentDate = From;
        var dividedDateRanges = new List<DateRange>();

        while (true)
        {
            var lastDayOfRange = currentDate.AddDays((int)daysLimit);

            if (lastDayOfRange >= To)
            {
                dividedDateRanges.Add(new DateRange(currentDate, To));
                break;
            }

            dividedDateRanges.Add(new DateRange(currentDate, lastDayOfRange));
            currentDate = lastDayOfRange.AddDays(1);
        }

        return dividedDateRanges;
    }

    public IEnumerable<DateRange> DividePerMonths(uint monthsLimit)
    {
        var currentDate = From;
        var dividedDateRanges = new List<DateRange>();

        while (true)
        {
            var lastDayOfRange = currentDate.AddMonths((int)monthsLimit);

            if (lastDayOfRange >= To)
            {
                dividedDateRanges.Add(new DateRange(currentDate, To));
                break;
            }

            dividedDateRanges.Add(new DateRange(currentDate, lastDayOfRange));
            currentDate = lastDayOfRange;
        }

        return dividedDateRanges;
    }

    public IEnumerable<DateRange> DividePerYears(uint yearLimit)
    {
        var currentDate = From;
        var dividedDateRanges = new List<DateRange>();

        while (true)
        {
            var lastDayOfRange = currentDate.AddYears((int)yearLimit);

            if (lastDayOfRange >= To)
            {
                dividedDateRanges.Add(new DateRange(currentDate, To));
                break;
            }

            dividedDateRanges.Add(new DateRange(currentDate, lastDayOfRange.AddDays(-1)));
            currentDate = lastDayOfRange;
        }

        return dividedDateRanges;
    }
    
    public bool IsIncludedInRange(DateOnly date) => date >= From && date <= To; 
    
    private static int CalculateDays(DateOnly from, DateOnly to) 
        => (int)(to.ToDateTime() - from.ToDateTime()).TotalDays;
}