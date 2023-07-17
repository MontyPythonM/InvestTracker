using InvestTracker.Users.Core.Dtos;

namespace InvestTracker.Users.Core.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetUserAsync(Guid id, CancellationToken token);
    Task<IEnumerable<UserDto>> GetUsersAsync(CancellationToken token);
    Task<UserDetailsDto?> GetUserDetailsAsync(Guid id, CancellationToken token);
    Task SetRoleAsync(SetRoleDto dto, CancellationToken token);
    Task RemoveRoleAsync(Guid id, CancellationToken token);
}