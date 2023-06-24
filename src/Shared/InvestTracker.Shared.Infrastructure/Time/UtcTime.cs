using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Shared.Infrastructure.Time;

internal class UtcTime : ITime
{
    public DateTime Current() => DateTime.UtcNow;
}