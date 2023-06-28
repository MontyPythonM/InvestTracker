namespace InvestTracker.Shared.Abstractions.Modules;

/// <summary>
/// Client who is able to publish actions (eg. events)
/// </summary>
public interface IModuleClient
{
    Task PublishAsync(object message);
}