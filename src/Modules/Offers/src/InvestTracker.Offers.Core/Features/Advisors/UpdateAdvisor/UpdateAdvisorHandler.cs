using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.Offers.Core.Features.Advisors.UpdateAdvisor;

internal sealed class UpdateAdvisorHandler : ICommandHandler<UpdateAdvisor>
{
    private readonly IAdvisorRepository _advisorRepository;

    public UpdateAdvisorHandler(IAdvisorRepository advisorRepository)
    {
        _advisorRepository = advisorRepository;
    }
    
    public async Task HandleAsync(UpdateAdvisor command, CancellationToken token)
    {
        var advisor = await _advisorRepository.GetAsync(command.Id, token);
        if (advisor is null)
        {
            throw new AdvisorNotFoundException(command.Id);
        }

        advisor.PhoneNumber = command.PhoneNumber;
        advisor.Bio = command.Bio;
        advisor.CompanyName = command.CompanyName;
        advisor.AvatarUrl = command.AvatarUrl;
        
        await _advisorRepository.UpdateAsync(advisor, token);
    }
}