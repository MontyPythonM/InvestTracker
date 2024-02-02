namespace InvestTracker.Shared.Abstractions.Modules;

/// <summary>
/// Client who is able to publish actions (eg. events)
/// </summary>
public interface IModuleClient
{
    Task PublishAsync(object message);
    Task SendAsync(string path, object request);
    Task<TResult?> SendAsync<TResult>(string path, object request) where TResult : class;
}