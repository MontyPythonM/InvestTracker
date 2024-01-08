namespace InvestTracker.Shared.Abstractions.Modules;

public interface IModule
{
    string Title { get; }
    string Path { get; }
    string SwaggerGroup { get; }
    string Version { get; }
}