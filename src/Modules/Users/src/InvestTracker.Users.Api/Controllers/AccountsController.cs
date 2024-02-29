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

internal class AccountsController : ApiControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IRequestContext _requestContext;

    public AccountsController(IAccountService accountService, IRequestContext requestContext)
    {
        _accountService = accountService;
        _requestContext = requestContext;
    }
    
    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation("Registration of a new system user")]
    public async Task<ActionResult> SignUp(SignUpDto dto, CancellationToken token)
    {
        await _accountService.SignUpAsync(dto, token);
        return Ok();
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation("Allows registered users enter into system")]
    public async Task<ActionResult<AuthenticationResponse>> SignIn(SignInDto dto, CancellationToken token)
        => await _accountService.SignInAsync(dto, token);

    [HttpDelete]
    [Authorize]
    [SwaggerOperation("Delete own account")]
    public async Task<ActionResult> DeleteCurrentUserAccount(DeleteAccountDto dto, CancellationToken token)
    {
        await _accountService.DeleteCurrentUserAccountAsync(dto, token);
        return NoContent();
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    [SwaggerOperation("Send email for reset password purpose")]
    public async Task<ActionResult> ForgotPassword(string email, CancellationToken token)
    {
        await _accountService.ForgotPasswordAsync(email, token);
        return Ok();
    }

    [HttpPost("reset-forgotten-password")]
    [AllowAnonymous]
    [SwaggerOperation("Reset password after invoking forgot-password action")]
    public async Task<ActionResult> ResetForgottenPassword(ResetPasswordDto dto, CancellationToken token)
    {
        await _accountService.ResetForgottenPasswordAsync(dto, token);
        return Ok();
    }
    
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    [SwaggerOperation("Reset password after invoking forgot-password action")]
    public async Task<ActionResult<AuthenticationResponse>> RefreshToken(AuthTokenDto dto, CancellationToken token) 
        => await _accountService.RefreshTokenAsync(dto, token);
    
    [HttpPost("revoke-token")]
    [Authorize]
    [SwaggerOperation("Revoke current user refresh token")]
    public async Task<ActionResult> RevokeToken(CancellationToken token)
    {
        await _accountService.RevokeTokenAsync(_requestContext.Identity.UserId, token);
        return Ok();
    }
    
    [HttpPost("revoke-token/{userId:guid}")]
    [HasPermission(UsersPermission.RevokeToken)]
    [SwaggerOperation("Revoke selected user refresh token")]
    public async Task<ActionResult> RevokeToken(Guid userId, CancellationToken token)
    {
        await _accountService.RevokeTokenAsync(userId, token);
        return Ok();
    }
}