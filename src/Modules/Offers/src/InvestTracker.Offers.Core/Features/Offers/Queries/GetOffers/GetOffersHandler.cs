using InvestTracker.Offers.Core.Persistence;
using InvestTracker.Shared.Abstractions.Pagination;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Offers.Core.Features.Offers.Queries.GetOffers;

internal sealed class GetOffersHandler : IQueryHandler<GetOffers, Paged<OfferDto>>
{
    private readonly OffersDbContext _context;

    public GetOffersHandler(OffersDbContext context)
    {
        _context = context;
    }
    
    public async Task<Paged<OfferDto>> HandleAsync(GetOffers query, CancellationToken token = default)
        => await _context.Offers
            .AsNoTracking()
            .Select(offer => new OfferDto
            {
                Id = offer.Id,
                Title = offer.Title,
                Description = offer.Description,
                AdvisorFullName = offer.Advisor.FullName
            })
            .OrderBy(offer => offer.Title)
            .PaginateAsync(query, token);
}