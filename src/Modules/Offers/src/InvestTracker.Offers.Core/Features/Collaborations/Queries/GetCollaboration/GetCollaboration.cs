using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.Offers.Core.Features.Collaborations.Queries.GetCollaboration;

internal record GetCollaboration(Guid Id, Guid UserId) : IQuery<CollaborationDetailsDto>;