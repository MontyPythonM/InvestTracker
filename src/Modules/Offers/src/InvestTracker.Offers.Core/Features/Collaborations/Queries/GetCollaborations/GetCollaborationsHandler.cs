using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.Offers.Core.Features.Collaborations.Queries.GetCollaborations;

internal sealed class GetCollaborationsHandler : IQueryHandler<GetCollaborations, IEnumerable<CollaborationDto>>
{
    public async Task<IEnumerable<CollaborationDto>> HandleAsync(GetCollaborations query, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}