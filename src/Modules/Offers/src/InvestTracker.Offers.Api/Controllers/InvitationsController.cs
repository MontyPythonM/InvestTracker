using InvestTracker.Offers.Api.Controllers.Base;
using InvestTracker.Offers.Core.Features.Invitations.Commands.ConfirmInvitation;
using InvestTracker.Offers.Core.Features.Invitations.Commands.RejectInvitation;
using InvestTracker.Offers.Core.Features.Invitations.Commands.SendInvitation;
using InvestTracker.Offers.Core.Features.Invitations.Queries.GetInvitations;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Offers.Api.Controllers;

internal class InvitationsController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public InvitationsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    [SwaggerOperation("Returns current user invitations")]
    public async Task<ActionResult<IEnumerable<InvitationDto>>> GetUserInvitations(CancellationToken token)
    {
        var currentUser = Guid.NewGuid(); // TODO: get user id from request
        return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetInvitations(currentUser), token));
    }

    [HttpPost]
    [SwaggerOperation("Allows investor to send an invitation to collaboration on the basis of the selected offer")]
    public async Task<ActionResult> SendCollaborationInvitation([FromBody] SendInvitation command, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }
    
    [HttpPatch("{id:guid}")]
    [SwaggerOperation("Advisor can confirm the invitation sent to him")]
    public async Task<ActionResult> ConfirmCollaborationInvitation(Guid id, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(new ConfirmInvitation(id), token);
        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    [SwaggerOperation("Advisor can reject the invitation sent to him")]
    public async Task<ActionResult> RejectCollaborationInvitation(Guid id, CancellationToken token)
    {        
        await _commandDispatcher.SendAsync(new RejectInvitation(id), token);
        return NoContent();
    }
}