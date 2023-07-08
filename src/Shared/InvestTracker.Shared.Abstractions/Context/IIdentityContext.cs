namespace InvestTracker.Shared.Abstractions.Context;

public interface IIdentityContext
{
    public Guid UserId { get; }
    bool IsAuthenticated { get; }
    string? Role { get; }
    string Subscription { get; }
}