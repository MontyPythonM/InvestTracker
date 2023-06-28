using InvestTracker.Offers.Core.Features.Offers.Queries.GetOfferDetails.Dtos;
using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.Offers.Core.Features.Offers.Queries.GetOfferDetails;

internal record GetOfferDetails(Guid Id) : IQuery<OfferDetailsDto>;