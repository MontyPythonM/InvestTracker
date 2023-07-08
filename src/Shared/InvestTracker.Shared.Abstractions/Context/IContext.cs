namespace InvestTracker.Shared.Abstractions.Context;

public interface IContext
{
    string RequestId { get; }
    string TraceId { get; }
    IIdentityContext Identity { get; }
}