using InvestTracker.Shared.Abstractions.Authentication;
using InvestTracker.Users.Api.Controllers.Base;
using InvestTracker.Users.Core.Dtos;
using InvestTracker.Users.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Users.Api.Controllers;

internal class AccountsController : ApiControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
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
    public async Task<ActionResult<JsonWebToken>> SignIn(SignInDto dto, CancellationToken token)
        => await _accountService.SignInAsync(dto, token);

    [HttpDelete]
    [Authorize]
    [SwaggerOperation("Delete own account")]
    public async Task<ActionResult> DeleteCurrentUserAccount(DeleteAccountDto dto, CancellationToken token)
    {
        await _accountService.DeleteCurrentUserAccount(dto, token);
        return NoContent();
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    [SwaggerOperation("Send email for reset password purpose")]
    public async Task<ActionResult> ForgotPassword(string email, CancellationToken token)
    {
        await _accountService.ForgotPassword(email, token);
        return Ok();
    }

    [HttpPost("reset-forgotten-password")]
    [AllowAnonymous]
    [SwaggerOperation("Reset password after invoking forgot-password action")]
    public async Task<ActionResult> ResetForgottenPassword(ResetPasswordDto dto, CancellationToken token)
    {
        await _accountService.ResetForgottenPassword(dto, token);
        return Ok();
    }
}