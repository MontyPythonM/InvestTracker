using InvestTracker.Shared.Abstractions.Modules;

namespace InvestTracker.InvestmentStrategies.Api;

internal class InvestmentStrategiesModule : IModule
{
    public const string BasePath = "investment-strategies-module";
    
    public string Title => "Investment Strategies Module";
    public string Path => BasePath;
    public string SwaggerGroup => BasePath;
    public string Version => "v1";
}