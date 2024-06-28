using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.Shared.Abstractions.Pagination;

public interface IPagedQuery : IQuery
{
    public int Page { get; set; }
    public int Results { get; set; }
}

public interface IPagedQuery<T> : IQuery<T>, IPagedQuery
{
}