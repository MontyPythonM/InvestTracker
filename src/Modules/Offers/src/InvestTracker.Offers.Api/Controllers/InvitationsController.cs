using InvestTracker.Offers.Api.Controllers.Base;
using InvestTracker.Offers.Core.Features.Invitations.Commands.ConfirmInvitation;
using InvestTracker.Offers.Core.Features.Invitations.Commands.RejectInvitation;
using InvestTracker.Offers.Core.Features.Invitations.Commands.SendInvitation;
using InvestTracker.Offers.Core.Features.Invitations.Queries.GetInvitations;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Offers.Api.Controllers;

internal class InvitationsController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IContext _context;

    public InvitationsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher,
        IContext context)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
        _context = context;
    }

    [HttpGet]
    [Authorize]
    [SwaggerOperation("Returns current user invitations")]
    public async Task<ActionResult<IEnumerable<InvitationDto>>> GetUserInvitations(CancellationToken token)
    {
        return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetInvitations(_context.Identity.UserId), token));
    }

    [HttpPost]
    [Authorize]
    [SwaggerOperation("Allows investor to send an invitation to collaboration on the basis of the selected offer")]
    public async Task<ActionResult> SendCollaborationInvitation([FromBody] SendInvitation command, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }
    
    [HttpPatch("{id:guid}")]
    [Authorize]
    [SwaggerOperation("Advisor can confirm the invitation sent to him")]
    public async Task<ActionResult> ConfirmCollaborationInvitation(Guid id, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(new ConfirmInvitation(id), token);
        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize]
    [SwaggerOperation("Advisor can reject the invitation sent to him")]
    public async Task<ActionResult> RejectCollaborationInvitation(Guid id, CancellationToken token)
    {        
        await _commandDispatcher.SendAsync(new RejectInvitation(id), token);
        return NoContent();
    }
}