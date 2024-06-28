using InvestTracker.Shared.Abstractions.Pagination;

namespace InvestTracker.Offers.Core.Features.Offers.Queries.GetOffers;

internal record GetOffers : IPagedQuery<Paged<OfferDto>>
{
    public int Page { get; set; }
    public int Results { get; set; }

    public GetOffers(int page, int results)
    {
        Page = page;
        Results = results;
    }
}