using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Shared.Abstractions.DDD.Exceptions;

public sealed class RoleNotExistException : InvestTrackerException
{
    public RoleNotExistException(string role) : base($"Role '{role}' is not defined in SystemRoles collection.")
    {
    }
}