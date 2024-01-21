using InvestTracker.Shared.Abstractions.Authentication;
using InvestTracker.Users.Core.Dtos;

namespace InvestTracker.Users.Core.Interfaces;

public interface IAccountService
{
    Task SignUpAsync(SignUpDto dto, CancellationToken token);
    Task<JsonWebToken> SignInAsync(SignInDto dto, CancellationToken token);
    Task DeleteCurrentUserAccount(DeleteAccountDto dto, CancellationToken token);
}