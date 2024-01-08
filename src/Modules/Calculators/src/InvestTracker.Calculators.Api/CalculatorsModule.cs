using InvestTracker.Shared.Abstractions.Modules;

namespace InvestTracker.Calculators.Api;

internal class CalculatorsModule : IModule
{
    public const string BasePath = "calculators-module";
    
    public string Title => "Calculators Module";
    public string Path => BasePath;
    public string SwaggerGroup => BasePath;
    public string Version => "v1";
}