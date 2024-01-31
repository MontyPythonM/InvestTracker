using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Repositories;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.IntegrationEvents;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.InvestmentStrategies.Application.Stakeholders.Events.External.Handlers;

internal sealed class AccountDeletedHandler : IEventHandler<AccountDeleted>
{
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly ITimeProvider _timeProvider;
    private readonly IInvestmentStrategyRepository _strategyRepository;
    
    public AccountDeletedHandler(IStakeholderRepository stakeholderRepository, ITimeProvider timeProvider, 
        IInvestmentStrategyRepository strategyRepository)
    {
        _stakeholderRepository = stakeholderRepository;
        _timeProvider = timeProvider;
        _strategyRepository = strategyRepository;
    }

    public async Task HandleAsync(AccountDeleted @event)
    {
        var stakeholder = await _stakeholderRepository.GetAsync(@event.Id);
        if (stakeholder is null)
        {
            return;
        }

        stakeholder.Lock(_timeProvider.Current(), @event.Id);

        var ownedStrategies = (await _strategyRepository.GetOwnerStrategiesAsync(@event.Id)).ToList();
        ownedStrategies.ForEach(strategy => strategy.Lock());

        await _stakeholderRepository.UpdateAsync(stakeholder);
        await _strategyRepository.UpdateRangeAsync(ownedStrategies);
    }
}