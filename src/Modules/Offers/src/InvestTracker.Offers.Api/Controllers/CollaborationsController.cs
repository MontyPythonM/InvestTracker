using InvestTracker.Offers.Api.Controllers.Base;
using InvestTracker.Offers.Core.Features.Collaborations.Commands.CancelCollaboration;
using InvestTracker.Offers.Core.Features.Collaborations.Queries.GetCollaboration;
using InvestTracker.Offers.Core.Features.Collaborations.Queries.GetCollaborations;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Offers.Api.Controllers;

internal class CollaborationsController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public CollaborationsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    [SwaggerOperation("Returns current user collaborations")]
    public async Task<ActionResult<IEnumerable<CollaborationDto>>> GetUserCollaborations(CancellationToken token)
    {
        var currentUser = Guid.NewGuid(); // TODO: get user id from request
        return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetCollaborations(currentUser), token));
    }
    
    [HttpGet("{id:guid}")]
    [SwaggerOperation("Returns current user collaboration details")]
    public async Task<ActionResult<CollaborationDetailsDto>> GetUserCollaboration(Guid id, CancellationToken token)
    {
        var currentUser = Guid.NewGuid(); // TODO: get user id from request
        return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetCollaboration(id, currentUser), token));
    }
    
    [HttpDelete]
    [SwaggerOperation("Cancel selected collaboration between Investor and Advisor")]
    public async Task<ActionResult> CancelCollaboration(CancelCollaboration command, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }
}