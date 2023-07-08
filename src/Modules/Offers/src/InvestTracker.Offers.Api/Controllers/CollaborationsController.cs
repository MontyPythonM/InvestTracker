using InvestTracker.Offers.Api.Controllers.Base;
using InvestTracker.Offers.Core.Features.Collaborations.Commands.CancelCollaboration;
using InvestTracker.Offers.Core.Features.Collaborations.Queries.GetCollaboration;
using InvestTracker.Offers.Core.Features.Collaborations.Queries.GetCollaborations;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Offers.Api.Controllers;

internal class CollaborationsController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IContext _context;

    public CollaborationsController(ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher, IContext context)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation("Returns current user collaborations")]
    public async Task<ActionResult<IEnumerable<CollaborationDto>>> GetUserCollaborations(CancellationToken token)
    {
        return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetCollaborations(_context.Identity.UserId), token));
    }
    
    [HttpGet("{id:guid}")]
    [SwaggerOperation("Returns current user collaboration details")]
    public async Task<ActionResult<CollaborationDetailsDto>> GetUserCollaboration(Guid id, CancellationToken token)
    {
        return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetCollaboration(id, _context.Identity.UserId), token));
    }
    
    [HttpDelete]
    [SwaggerOperation("Cancel selected collaboration between Investor and Advisor")]
    public async Task<ActionResult> CancelCollaboration(CancelCollaboration command, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }
}