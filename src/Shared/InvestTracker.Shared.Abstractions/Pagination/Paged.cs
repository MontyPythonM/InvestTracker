namespace InvestTracker.Shared.Abstractions.Pagination;

public class Paged<T>
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    public int CurrentPage { get; set; }
    public int ResultsPerPage { get; set; }
    public int TotalPages { get; set; }
    public long TotalResults { get; set; }
    
    public bool IsEmpty => Items is null || !Items.Any();

    public Paged(IReadOnlyList<T> items, int currentPage, int resultsPerPage, int totalPages, long totalResults)
    {
        Items = items;
        CurrentPage = currentPage;
        ResultsPerPage = resultsPerPage;
        TotalPages = totalPages;
        TotalResults = totalResults;
    }
}