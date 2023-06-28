namespace InvestTracker.Shared.Infrastructure.Modules;

/// <summary>
/// Register of actions (e.g. events) from modules
/// </summary>
public interface IModuleRegistry
{
    /// <summary>
    /// Add new action registration
    /// </summary>
    void AddBroadcastAction(Type requestType, Func<object, Task> action);

    /// <summary>
    /// Gets all registrations for the indicated key
    /// </summary>
    /// <param name="key">Action path</param>
    /// <returns></returns>
    IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key);
}