using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Offers.Core.Events.External.Handlers;

internal sealed class InvestorAppointedHandler : IEventHandler<InvestorAppointed>
{
    private readonly IInvestorRepository _investorRepository;

    public InvestorAppointedHandler(IInvestorRepository investorRepository)
    {
        _investorRepository = investorRepository;
    }
    
    public async Task HandleAsync(InvestorAppointed @event)
    {
        if (await _investorRepository.ExistsAsync(@event.Id, CancellationToken.None))
        {
            throw new InvestorAlreadyExistsException(@event.Id);
        }
        
        var investor = new Investor
        {
            Id = Guid.NewGuid(),
            Email = @event.Email,
            FullName = @event.FullName,
        };

        await _investorRepository.CreateAsync(investor, CancellationToken.None);
    }
}