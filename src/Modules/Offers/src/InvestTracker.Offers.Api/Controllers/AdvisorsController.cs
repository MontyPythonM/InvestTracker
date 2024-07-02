using InvestTracker.Offers.Api.Controllers.Base;
using InvestTracker.Offers.Api.Dtos;
using InvestTracker.Offers.Api.Permissions;
using InvestTracker.Offers.Core.Features.Advisors.Commands.UpdateAdvisor;
using InvestTracker.Offers.Core.Features.Advisors.Queries.GetAdvisor;
using InvestTracker.Offers.Core.Features.Offers.Queries.GetOfferDetails.Dtos;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Offers.Api.Controllers;

internal class AdvisorsController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public AdvisorsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet("{id:guid}")]
    [HasPermission(OffersPermission.UpdateAdvisorDetails)]
    [SwaggerOperation("Get information about advisor")]
    public async Task<ActionResult<AdvisorDetailsDto>> GetAdvisorDetails(Guid id, CancellationToken token)
    {
        return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetAdvisor(id), token));
    }

    [HttpPatch]
    [HasPermission(OffersPermission.UpdateAdvisorDetails)]
    [SwaggerOperation("Change information about advisor by himself or administrator")]
    public async Task<ActionResult> UpdateAdvisorDetails([FromBody] UpdateAdvisorDto dto, CancellationToken token)
    {
        var command = new UpdateAdvisor(dto.Id, dto.PhoneNumber, dto.Bio, dto.CompanyName, dto.Avatar);
        
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }
}