using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Users.Core.Exceptions;

internal class RoleNotFoundException : InvestTrackerException
{
    public RoleNotFoundException(string value) : base($"Role {value} not found")
    {
    }
}