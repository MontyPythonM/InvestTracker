using InvestTracker.Offers.Api.Controllers.Base;
using InvestTracker.Offers.Api.Permissions;
using InvestTracker.Offers.Core.Features.Invitations.Commands.ConfirmInvitation;
using InvestTracker.Offers.Core.Features.Invitations.Commands.RejectInvitation;
using InvestTracker.Offers.Core.Features.Invitations.Commands.SendInvitation;
using InvestTracker.Offers.Core.Features.Invitations.Queries.GetInvitations;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Offers.Api.Controllers;

internal class InvitationsController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IRequestContext _requestContext;

    public InvitationsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher,
        IRequestContext requestContext)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
        _requestContext = requestContext;
    }

    [HttpGet]
    [HasPermission(OffersPermission.GetUserInvitations)]
    [SwaggerOperation("Returns current user invitations")]
    public async Task<ActionResult<IEnumerable<InvitationDto>>> GetUserInvitations(CancellationToken token)
    {
        return OkOrNotFound(await _queryDispatcher.QueryAsync(new GetInvitations(_requestContext.Identity.UserId), token));
    }

    [HttpPost]
    [HasPermission(OffersPermission.SendCollaborationInvitation)]
    [SwaggerOperation("Allows investor to send an invitation to collaboration on the basis of the selected offer")]
    public async Task<ActionResult> SendCollaborationInvitation([FromBody] SendInvitation command, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }
    
    [HttpPatch("{id:guid}")]
    [HasPermission(OffersPermission.ConfirmCollaborationInvitation)]
    [SwaggerOperation("Advisor can confirm the invitation sent to him")]
    public async Task<ActionResult> ConfirmCollaborationInvitation(Guid id, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(new ConfirmInvitation(id), token);
        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    [HasPermission(OffersPermission.RejectCollaborationInvitation)]
    [SwaggerOperation("Advisor can reject the invitation sent to him")]
    public async Task<ActionResult> RejectCollaborationInvitation(Guid id, CancellationToken token)
    {        
        await _commandDispatcher.SendAsync(new RejectInvitation(id), token);
        return NoContent();
    }
}