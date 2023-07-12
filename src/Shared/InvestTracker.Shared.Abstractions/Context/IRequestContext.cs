namespace InvestTracker.Shared.Abstractions.Context;

public interface IRequestContext
{
    string RequestId { get; }
    string TraceId { get; }
    IIdentityContext Identity { get; }
}