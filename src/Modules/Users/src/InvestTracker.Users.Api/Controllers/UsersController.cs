using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Infrastructure.Authorization;
using InvestTracker.Users.Api.Controllers.Base;
using InvestTracker.Users.Api.Permissions;
using InvestTracker.Users.Core.Dtos;
using InvestTracker.Users.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Users.Api.Controllers;

internal class UsersController : ApiControllerBase
{
    private readonly IUserService _userService;
    private readonly IRequestContext _requestContext;

    public UsersController(IUserService userService, IRequestContext requestContext)
    {
        _userService = userService;
        _requestContext = requestContext;
    }
    
    [HttpGet]
    [HasPermission(UsersPermission.GetUsers)]
    [SwaggerOperation("Returns list of application users")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(CancellationToken token)
        => OkOrNotFound(await _userService.GetUsersAsync(token));
    
    [HttpGet("current")]
    [Authorize]
    [SwaggerOperation("Returns current user details")]
    public async Task<ActionResult<UserDto?>> GetUser(CancellationToken token)
        => OkOrNotFound(await _userService.GetUserAsync(_requestContext.Identity.UserId, token));

    [HttpGet("{id:guid}")]
    [HasPermission(UsersPermission.GetUserDetails)]
    [SwaggerOperation("Returns selected user details")]
    public async Task<ActionResult<UserDetailsDto?>> GetUserDetails(Guid id, CancellationToken token)
        => OkOrNotFound(await _userService.GetUserDetailsAsync(id, token));

    [HttpPatch("{id:guid}/set-role")]
    [HasPermission(UsersPermission.SetRole)]
    [SwaggerOperation("Set selected user role")]
    public async Task<ActionResult> SetRole([FromBody] SetRoleDto dto, Guid id, CancellationToken token)
    {
        await _userService.SetRoleAsync(id, dto, token);
        return Ok();
    }
    
    [HttpPatch("{id:guid}/set-subscription")]
    [HasPermission(UsersPermission.SetSubscription)]
    [SwaggerOperation("Set selected user system subscription")]
    public async Task<ActionResult> SetSubscription([FromBody] SetSubscriptionDto dto, Guid id, CancellationToken token)
    {
        await _userService.SetSubscriptionAsync(id, dto, token);
        return Ok();
    }
    
    [HttpPatch("{id:guid}/activate")]
    [HasPermission(UsersPermission.ActivateUserAccount)]
    [SwaggerOperation("Activate selected user account")]
    public async Task<ActionResult> ActivateAccount(Guid id, CancellationToken token)
    {
        await _userService.SetAccountActivationAsync(id, true, token);
        return Ok();
    }
    
    [HttpPatch("{id:guid}/deactivate")]
    [HasPermission(UsersPermission.DeactivateUserAccount)]
    [SwaggerOperation("Deactivate selected user account")]
    public async Task<ActionResult> DeactivateAccount(Guid id, CancellationToken token)
    {
        await _userService.SetAccountActivationAsync(id, false, token);
        return Ok();
    }
}