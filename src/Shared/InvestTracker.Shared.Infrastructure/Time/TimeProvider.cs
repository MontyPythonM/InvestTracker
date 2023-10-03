﻿using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.Shared.Infrastructure.Time;

internal class TimeProvider : ITimeProvider
{
    public DateTime Current() => DateTime.Now;
}