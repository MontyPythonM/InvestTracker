using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Infrastructure.Authorization;
using InvestTracker.Users.Api.Controllers.Base;
using InvestTracker.Users.Api.Permissions;
using InvestTracker.Users.Core.Dtos;
using InvestTracker.Users.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Users.Api.Controllers;

internal class UsersController : ApiControllerBase
{
    private readonly IUserService _userService;
    private readonly IContext _context;

    public UsersController(IUserService userService, IContext context)
    {
        _userService = userService;
        _context = context;
    }
    
    [HttpGet]
    [SwaggerOperation("Returns current user details")]
    public async Task<ActionResult<UserDto?>> GetUser(CancellationToken token)
        => OkOrNotFound(await _userService.GetUserAsync(_context.Identity.UserId, token));
    
    [HttpGet("all")]
    [HasPermission(UserPermission.GetUsers)]
    [SwaggerOperation("Returns list of application users")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(CancellationToken token)
        => OkOrNotFound(await _userService.GetUsersAsync(token));
    
    [HttpGet("{id:guid}/details")]
    [HasPermission(UserPermission.GetUserDetails)]
    [SwaggerOperation("Returns selected user details")]
    public async Task<ActionResult<UserDetailsDto?>> GetUserDetails(Guid id, CancellationToken token)
        => OkOrNotFound(await _userService.GetUserDetailsAsync(id, token));
}