using InvestTracker.InvestmentStrategies.Domain.Collaborations.Repositories;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.InvestmentStrategies.Application.Collaborations.Events.External.Handlers;

internal sealed class CollaborationCancelledHandler : IEventHandler<CollaborationCancelled>
{
    private readonly ICollaborationRepository _collaborationRepository;
    private readonly IInvestmentStrategyRepository _investmentStrategyRepository;

    public CollaborationCancelledHandler(ICollaborationRepository collaborationRepository, 
        IInvestmentStrategyRepository investmentStrategyRepository)
    {
        _collaborationRepository = collaborationRepository;
        _investmentStrategyRepository = investmentStrategyRepository;
    }

    public async Task HandleAsync(CollaborationCancelled @event)
    {
        var collaboration = await _collaborationRepository.GetAsync(@event.AdvisorId, @event.InvestorId);
        if (collaboration is null)
        {
            return;
        }

        var sharedStrategies = (await _investmentStrategyRepository.GetByCollaborationAsync(@event.AdvisorId, @event.InvestorId))
            .DistinctBy(strategy => strategy.Id)
            .ToList();

        sharedStrategies.ForEach(strategy => strategy.RemoveCollaborator(@event.AdvisorId, @event.InvestorId));

        await _investmentStrategyRepository.UpdateRangeAsync(sharedStrategies);
        await _collaborationRepository.DeleteAsync(collaboration);
    }
}