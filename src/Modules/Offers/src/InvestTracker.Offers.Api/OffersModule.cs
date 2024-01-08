using InvestTracker.Shared.Abstractions.Modules;

namespace InvestTracker.Offers.Api;

internal class OffersModule : IModule
{
    public const string BasePath = "offers-module";
    
    public string Title => "Offers Module";
    public string Path => BasePath;
    public string SwaggerGroup => BasePath;
    public string Version => "v1";
}