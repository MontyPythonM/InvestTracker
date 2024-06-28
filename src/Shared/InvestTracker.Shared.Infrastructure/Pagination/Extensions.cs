using InvestTracker.Shared.Abstractions.Pagination;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Shared.Infrastructure.Pagination;

public static class Extensions
{
    public static Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, IPagedQuery query, CancellationToken token = default)
        => data.PaginateAsync(query.Page, query.Results, token);

    public static async Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, int page, int results, 
        CancellationToken token = default)
    {
        if (page < 0)
        {
            page = 1;
        }

        results = LimitResults(results);

        var totalResults = await data.CountAsync(token);
        var totalPages = totalResults <= results ? 1 : (int)Math.Ceiling((double)totalResults / results);
        var result = await data.Skip((page - 1) * results).Take(results).ToListAsync(token);

        return new Paged<T>(result, page, results, totalPages, totalResults);
    }

    private static int LimitResults(int results)
    {
        return results switch
        {
            <= 0 => 10,
            > 100 => 100,
            _ => results
        };
    }
}