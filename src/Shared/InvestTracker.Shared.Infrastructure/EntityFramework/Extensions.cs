using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Shared.Infrastructure.EntityFramework;

public static class Extensions
{
    public static IQueryable<T> ApplyAsNoTracking<T>(this IQueryable<T> query, bool asNoTracking) 
        where T : class 
        => asNoTracking ? query.AsNoTracking() : query;
}