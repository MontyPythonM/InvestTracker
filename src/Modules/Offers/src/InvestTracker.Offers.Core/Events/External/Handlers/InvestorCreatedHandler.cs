using InvestTracker.Offers.Core.Entities;
using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.IntegrationEvents;

namespace InvestTracker.Offers.Core.Events.External.Handlers;

internal sealed class InvestorCreatedHandler : IEventHandler<InvestorCreated>
{
    private readonly IInvestorRepository _investorRepository;

    public InvestorCreatedHandler(IInvestorRepository investorRepository)
    {
        _investorRepository = investorRepository;
    }
    
    public async Task HandleAsync(InvestorCreated @event)
    {
        if (await _investorRepository.ExistsAsync(@event.Id, CancellationToken.None))
        {
            throw new InvestorAlreadyExistsException(@event.Id);
        }
        
        var investor = new Investor
        {
            Id = @event.Id,
            Email = @event.Email,
            FullName = @event.FullName
        };

        await _investorRepository.CreateAsync(investor, CancellationToken.None);
    }
}