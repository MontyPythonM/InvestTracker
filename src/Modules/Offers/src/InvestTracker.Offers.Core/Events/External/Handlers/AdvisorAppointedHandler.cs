using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Offers.Core.Events.External.Handlers;

internal sealed class AdvisorAppointedHandler : IEventHandler<AdvisorAppointed>
{
    private readonly IAdvisorRepository _advisorRepository;

    public AdvisorAppointedHandler(IAdvisorRepository advisorRepository)
    {
        _advisorRepository = advisorRepository;
    }
    
    public async Task HandleAsync(AdvisorAppointed @event)
    {
        if (await _advisorRepository.ExistsAsync(@event.Id, CancellationToken.None))
        {
            throw new AdvisorAlreadyExistsException(@event.Id);
        }
        
        var advisor = new Advisor
        {
            Id = Guid.NewGuid(),
            Email = @event.Email,
            FullName = @event.FullName,
        };

        await _advisorRepository.CreateAsync(advisor, CancellationToken.None);
    }
}