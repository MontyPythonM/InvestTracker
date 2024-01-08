using InvestTracker.Shared.Abstractions.Modules;

namespace InvestTracker.Exports.Api;

internal class ExportsModule : IModule
{
    public const string BasePath = "exports-module";
    
    public string Title => "Exports Module";
    public string Path => BasePath;
    public string SwaggerGroup => BasePath;
    public string Version => "v1";
}