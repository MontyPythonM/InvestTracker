using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.Offers.Core.Features.Collaborations.Queries.GetCollaboration;

internal sealed class GetCollaborationHandler : IQueryHandler<GetCollaboration, CollaborationDetailsDto>
{
    public async Task<CollaborationDetailsDto> HandleAsync(GetCollaboration query, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}