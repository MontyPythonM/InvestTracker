using InvestTracker.Shared.Abstractions.Time;
using InvestTracker.Shared.Abstractions.Types;

namespace InvestTracker.Shared.Infrastructure.Time;

internal class TimeProvider : ITimeProvider
{
    public DateTime Current() => DateTime.Now;
    public DateOnly CurrentDate() => DateTime.Now.ToDateOnly();
}