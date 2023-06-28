using InvestTracker.Offers.Core.Persistence;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.Offers.Core.Features.Offers.Queries.GetOffers;

internal sealed class GetOffersHandler : IQueryHandler<GetOffers, IEnumerable<OfferDto>>
{
    private readonly OffersDbContext _context;

    public GetOffersHandler(OffersDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<OfferDto>> HandleAsync(GetOffers query, CancellationToken token = default)
        => await _context.Offers
            .AsNoTracking()
            .Select(offer => new OfferDto
            {
                Id = offer.Id,
                Title = offer.Title,
                Description = offer.Description,
                AdvisorFullName = offer.Advisor.FullName
            })
            .ToListAsync(token);
}