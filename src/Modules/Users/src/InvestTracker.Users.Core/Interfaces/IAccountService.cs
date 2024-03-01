using InvestTracker.Users.Core.Dtos;

namespace InvestTracker.Users.Core.Interfaces;

public interface IAccountService
{
    Task SignUpAsync(SignUpDto dto, CancellationToken token);
    Task<AuthenticationResponse> SignInAsync(SignInDto dto, CancellationToken token);
    Task DeleteCurrentUserAccountAsync(DeleteAccountDto dto, CancellationToken token);
    Task ForgotPasswordAsync(string email, CancellationToken token);
    Task ResetForgottenPasswordAsync(ResetPasswordDto dto, CancellationToken token);
    Task<AuthenticationResponse> RefreshTokenAsync(string? refreshToken, CancellationToken token);
    Task RevokeRefreshTokenAsync(Guid userId, CancellationToken token);
}