using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.ValueObjects;

public record Role
{
    public string Value { get; }

    public Role(string value)
    {
        if (!IsValidRole(value))
        {
            throw new RoleNotExistException(value);
        }

        Value = value;
    }
    
    public static implicit operator string(Role role) => role.Value;
    public static implicit operator Role(string role) => new(role);
    
    private static bool IsValidRole(string value) 
        => SystemRole.Roles.Contains(value) || string.IsNullOrEmpty(value);
}