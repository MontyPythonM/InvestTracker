using InvestTracker.Offers.Api.Controllers.Base;
using InvestTracker.Offers.Api.Permissions;
using InvestTracker.Offers.Core.Features.Collaborations.Commands.CancelCollaboration;
using InvestTracker.Offers.Core.Features.Collaborations.Queries.GetCollaboration;
using InvestTracker.Offers.Core.Features.Collaborations.Queries.GetCollaborations;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Offers.Api.Controllers;

internal class CollaborationsController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IRequestContext _requestContext;

    public CollaborationsController(ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher, IRequestContext requestContext)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
        _requestContext = requestContext;
    }

    [HttpGet]
    [HasPermission(OffersPermission.GetUserCollaborations)]
    [SwaggerOperation("Returns current user collaborations")]
    public async Task<ActionResult<IEnumerable<CollaborationDto>>> GetUserCollaborations(CancellationToken token)
    {
        return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetCollaborations(_requestContext.Identity.UserId), token));
    }
    
    [HttpGet("details")]
    [HasPermission(OffersPermission.GetUserCollaboration)]
    [SwaggerOperation("Returns current user collaboration details")]
    public async Task<ActionResult<CollaborationDetailsDto>> GetUserCollaboration([FromQuery] GetCollaboration query, 
        CancellationToken token)
    {
        return OkOrNotFound(await _queryDispatcher.QueryAsync(query, token))!;
    }
    
    [HttpDelete("own")]
    [HasPermission(OffersPermission.CancelOwnCollaboration)]
    [SwaggerOperation("Cancel selected collaboration between Investor and Advisor, only if current user is one of them")]
    public async Task<ActionResult> CancelOwnCollaboration(CancelCollaboration command, CancellationToken token)
    {
        if (_requestContext.Identity.UserId != command.AdvisorId && 
            _requestContext.Identity.UserId != command.InvestorId)
        {
            return Forbid();
        }

        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }
    
    [HttpDelete]
    [HasPermission(OffersPermission.CancelSelectedCollaboration)]
    [SwaggerOperation("Cancel selected collaboration between Investor and Advisor")]
    public async Task<ActionResult> CancelSelectedCollaboration(CancelCollaboration command, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }
}