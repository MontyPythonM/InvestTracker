using System.Buffers.Text;
using InvestTracker.Offers.Core.Exceptions;
using InvestTracker.Offers.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Commands;

namespace InvestTracker.Offers.Core.Features.Advisors.Commands.UpdateAdvisor;

internal sealed class UpdateAdvisorHandler : ICommandHandler<UpdateAdvisor>
{
    private readonly IAdvisorRepository _advisorRepository;

    public UpdateAdvisorHandler(IAdvisorRepository advisorRepository)
    {
        _advisorRepository = advisorRepository;
    }
    
    public async Task HandleAsync(UpdateAdvisor command, CancellationToken token)
    {
        var advisor = await _advisorRepository.GetAsync(command.AdvisorId, token);
        if (advisor is null)
        {
            throw new AdvisorNotFoundException(command.AdvisorId);
        }

        advisor.PhoneNumber = command.PhoneNumber;
        advisor.Bio = command.Bio;
        advisor.CompanyName = command.CompanyName;

        if (command.Avatar is not null && Base64.IsValid(command.Avatar))
        {
            advisor.Avatar = Convert.FromBase64String(command.Avatar);
        }
        
        await _advisorRepository.UpdateAsync(advisor, token);
    }
}