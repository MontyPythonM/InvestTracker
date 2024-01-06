using System.Globalization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.Shared.Abstractions.Types;

public static class DateExtensions
{
    public static DateTime ToDateTime(this DateOnly date) 
        => date.ToDateTime(TimeOnly.MinValue);

    public static DateOnly ToDateOnly(this DateTime dateTime) 
        => DateOnly.FromDateTime(dateTime);

    public static IEnumerable<DateTime> ToDateTime(this IEnumerable<DateOnly> dates) 
        => dates.Select(d => d.ToDateTime());

    public static IEnumerable<DateOnly> ToDateOnly(this IEnumerable<DateTime> dateTimes) 
        => dateTimes.Select(d => d.ToDateOnly());

    public static int GetDaysInYear(this DateTime dateTime) 
        => new GregorianCalendar().GetDaysInYear(dateTime.Year);

    public static int GetDaysInYear(this DateOnly date)
        => GetDaysInYear(date.ToDateTime());
    
    public static int GetDayNumberOfYear(this DateTime dateTime)
        => new GregorianCalendar().GetDayOfYear(dateTime);

    public static int GetDayNumberOfYear(this DateOnly date)
        => GetDayNumberOfYear(date.ToDateTime());
    
    public static IEnumerable<DateRange> GetYearlyRanges(this DateOnly startDate, uint years)
    {
        var yearlyRanges = new List<DateRange>();
        var currentDate = startDate;

        for (var year = 0; year < years; year++)
        {
            var firstDayOfRange = currentDate;
            var lastDayOfRange = currentDate.AddYears(1).AddDays(-1);
            var range = new DateRange(firstDayOfRange, lastDayOfRange);
            yearlyRanges.Add(range);

            currentDate = lastDayOfRange.AddDays(1);
        }

        return yearlyRanges;
    }
}