using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.Offers.Core.Features.Offers.Queries.GetOffers;

internal record GetOffers() : IQuery<IEnumerable<OfferDto>>;