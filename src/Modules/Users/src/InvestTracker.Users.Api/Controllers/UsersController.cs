using InvestTracker.Shared.Abstractions.SystemPermissions;
using InvestTracker.Users.Api.Controllers.Base;
using InvestTracker.Users.Core.Dtos;
using InvestTracker.Users.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Users.Api.Controllers;

internal class UsersController : ApiControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    // TODO: use httpContext user id instead of id from route
    [HttpGet("{id:guid}")]
    [SwaggerOperation("Returns current user details")]
    public async Task<ActionResult<UserDto?>> GetUser(Guid id, CancellationToken token)
        => OkOrNotFound(await _userService.GetUserAsync(id, token));
    
    [HttpGet]    
    [Authorize(Roles = SystemRole.SystemAdministrator)]
    [SwaggerOperation("Returns list of application users")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(CancellationToken token)
        => OkOrNotFound(await _userService.GetUsersAsync(token));
    
    [HttpGet("{id:guid}/details")]
    [Authorize(Roles = SystemRole.SystemAdministrator)]
    [SwaggerOperation("Returns selected user details")]
    public async Task<ActionResult<UserDetailsDto?>> GetUserDetails(Guid id, CancellationToken token)
        => OkOrNotFound(await _userService.GetUserDetailsAsync(id, token));
}