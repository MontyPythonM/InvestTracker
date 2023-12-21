using System.Globalization;

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
}