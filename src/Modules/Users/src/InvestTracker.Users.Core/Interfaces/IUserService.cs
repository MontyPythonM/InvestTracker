using InvestTracker.Users.Core.Dtos;

namespace InvestTracker.Users.Core.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetUserAsync(Guid id, CancellationToken token);
    Task<IEnumerable<UserDto>> GetUsersAsync(CancellationToken token);
    Task<UserDetailsDto?> GetUserDetailsAsync(Guid id, CancellationToken token);
    Task SetRoleAsync(Guid userId, SetRoleDto dto, CancellationToken token);
    Task RemoveRoleAsync(Guid id, CancellationToken token);
    Task SetSubscriptionAsync(Guid userId, SetSubscriptionDto dto, CancellationToken token);
}