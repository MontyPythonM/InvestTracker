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
    /// Add new action registration with unique path for internal modules synchronous communication
    /// </summary>
    void AddRequestAction(string path, Type requestType, Type responseType, Func<object, Task<object>> action);
    
    /// <summary>
    /// Gets all registrations for the indicated key
    /// </summary>
    IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key);
    
    /// <summary>
    /// Returns request registration
    /// </summary>
    ModuleRequestRegistration? GetRequestRegistration(string path);
}