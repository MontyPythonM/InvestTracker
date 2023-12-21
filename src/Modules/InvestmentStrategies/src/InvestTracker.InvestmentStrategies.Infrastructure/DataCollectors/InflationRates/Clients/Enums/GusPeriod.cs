namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Clients.Enums;

/// <summary>
/// Period Ids ("id-okres") for GUS external API
/// </summary>
internal static class GusApiPeriods
{
    internal static GusPeriod January = new(1, 247);
    internal static GusPeriod February = new(2, 248);
    internal static GusPeriod March = new(3, 249);
    internal static GusPeriod April = new(4, 250);
    internal static GusPeriod May = new(5, 251);
    internal static GusPeriod June = new(6, 252);
    internal static GusPeriod July = new(7, 253);
    internal static GusPeriod August = new(8, 254);
    internal static GusPeriod September = new(9, 255);
    internal static GusPeriod October = new(10, 256);
    internal static GusPeriod November = new(11, 257);
    internal static GusPeriod December = new(12, 258);

    internal record GusPeriod(int MonthNumber, int PeriodId);

    internal static GusPeriod GetByMonth(int month)
    {
        return month switch
        {
            1 => January,
            2 => February,
            3 => March,
            4 => April,
            5 => May,
            6 => June,
            7 => July,
            8 => August,
            9 => September,
            10 => October,
            11 => November,
            12 => December,
            _ => throw new ArgumentOutOfRangeException(month.ToString())
        };
    }
}