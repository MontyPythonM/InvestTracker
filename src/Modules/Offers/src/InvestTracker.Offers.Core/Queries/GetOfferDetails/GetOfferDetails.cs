using InvestTracker.Offers.Core.Queries.GetOfferDetails.Dtos;
using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.Offers.Core.Queries.GetOfferDetails;

internal record GetOfferDetails(Guid Id) : IQuery<OfferDetailsDto>;