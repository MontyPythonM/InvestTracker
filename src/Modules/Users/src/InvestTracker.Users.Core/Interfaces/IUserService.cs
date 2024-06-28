using InvestTracker.Shared.Abstractions.Pagination;
using InvestTracker.Users.Core.Dtos;

namespace InvestTracker.Users.Core.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetUserAsync(Guid id, CancellationToken token);
    Task<Paged<UserDto>> GetUsersAsync(IPagedQuery query, CancellationToken token);
    Task<UserDetailsDto?> GetUserDetailsAsync(Guid id, CancellationToken token);
    Task SetRoleAsync(Guid userId, SetRoleDto dto, CancellationToken token);
    Task SetSubscriptionAsync(Guid userId, SetSubscriptionDto dto, CancellationToken token);
    Task SetAccountActivationAsync(Guid userId, bool isActive, CancellationToken token);
}