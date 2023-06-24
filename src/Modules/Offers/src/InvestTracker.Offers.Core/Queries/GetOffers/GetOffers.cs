using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.Offers.Core.Queries.GetOffers;

internal record GetOffers() : IQuery<IEnumerable<OfferDto>>;