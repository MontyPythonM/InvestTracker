using InvestTracker.Shared.Abstractions.DDD.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.ValueObjects;

public sealed record MonthlyDate
{
    public int Year { get; }
    public int Month { get; }

    public MonthlyDate(int year, int month)
    {
        if (!IsYearValid(year))
        {
            throw new InvalidMonthlyDateException($"Year out of range");
        }

        if (!IsMonthValid(month))
        {
            throw new InvalidMonthlyDateException($"Incorrect month");
        }
        
        Year = year;
        Month = month;
    }
    
    public MonthlyDate(DateOnly date)
    {
        if (!IsYearValid(date.Year))
        {
            throw new InvalidMonthlyDateException($"Year out of range");
        }
        
        Year = date.Year;
        Month = date.Month;
    }

    public override string ToString() => $"{Month}/{Year}";

    private static bool IsYearValid(int year)
        => year is > 1900 and <= 2100;
    
    private static bool IsMonthValid(int month)
        => month is > 0 and <= 12;
}