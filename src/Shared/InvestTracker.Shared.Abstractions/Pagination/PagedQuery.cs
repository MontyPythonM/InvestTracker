namespace InvestTracker.Shared.Abstractions.Pagination;

public class PagedQuery : IPagedQuery
{
    public int Page { get; set; }
    public int Results { get; set; }

    public PagedQuery(int page, int results)
    {
        Page = page;
        Results = results;
    }
}

public class PagedQuery<T> : PagedQuery, IPagedQuery<Paged<T>>
{
    public PagedQuery(int page, int results) : base(page, results)
    {
    }
}