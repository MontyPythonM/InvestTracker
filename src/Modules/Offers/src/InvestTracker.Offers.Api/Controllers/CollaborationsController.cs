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
    private readonly IContext _context;

    public CollaborationsController(ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher, IContext context)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
        _context = context;
    }

    [HttpGet]
    [HasPermission(OffersPermission.GetUserCollaborations)]
    [SwaggerOperation("Returns current user collaborations")]
    public async Task<ActionResult<IEnumerable<CollaborationDto>>> GetUserCollaborations(CancellationToken token)
    {
        return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetCollaborations(_context.Identity.UserId), token));
    }
    
    [HttpGet("{id:guid}")]
    [HasPermission(OffersPermission.GetUserCollaboration)]
    [SwaggerOperation("Returns current user collaboration details")]
    public async Task<ActionResult<CollaborationDetailsDto>> GetUserCollaboration(Guid id, CancellationToken token)
    {
        return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetCollaboration(id, _context.Identity.UserId), token));
    }
    
    [HttpDelete("own")]
    [HasPermission(OffersPermission.CancelOwnCollaboration)]
    [SwaggerOperation("Cancel selected collaboration between Investor and Advisor, only if current user is one of them")]
    public async Task<ActionResult> CancelOwnCollaboration(CancelCollaboration command, CancellationToken token)
    {
        if (_context.Identity.UserId != command.AdvisorId && 
            _context.Identity.UserId != command.InvestorId)
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