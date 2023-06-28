using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.Offers.Core.Features.Collaborations.Queries.GetCollaborations;

internal record GetCollaborations(Guid UserId) : IQuery<IEnumerable<CollaborationDto>>;