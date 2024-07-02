using InvestTracker.Offers.Core.Features.Offers.Queries.GetOfferDetails.Dtos;
using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.Offers.Core.Features.Advisors.Queries.GetAdvisor;

public record GetAdvisor(Guid Id) : IQuery<AdvisorDetailsDto>;