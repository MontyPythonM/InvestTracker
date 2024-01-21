using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Entities;
using InvestTracker.InvestmentStrategies.Domain.Stakeholders.Repositories;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.IntegrationEvents;
using InvestTracker.Shared.Abstractions.Time;

namespace InvestTracker.InvestmentStrategies.Application.Stakeholders.Events.External.Handlers;

internal sealed class InvestorCreatedHandler : IEventHandler<InvestorCreated>
{
    private readonly IStakeholderRepository _stakeholderRepository;
    private readonly ITimeProvider _timeProvider;

    public InvestorCreatedHandler(IStakeholderRepository stakeholderRepository, ITimeProvider timeProvider)
    {
        _stakeholderRepository = stakeholderRepository;
        _timeProvider = timeProvider;
    }
    
    public async Task HandleAsync(InvestorCreated @event)
    {
        var stakeholderExists = await _stakeholderRepository.ExistsAsync(@event.Id);
        if (stakeholderExists)
        {
            return;
        }

        var stakeholder = new Stakeholder(@event.Id, @event.FullName, @event.Email, SystemSubscription.StandardInvestor, _timeProvider.Current());
        await _stakeholderRepository.AddAsync(stakeholder);
    }
}