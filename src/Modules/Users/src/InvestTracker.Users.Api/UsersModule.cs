using InvestTracker.Shared.Abstractions.Modules;

namespace InvestTracker.Users.Api;

internal class UsersModule : IModule
{
    public const string BasePath = "users-module";
    
    public string Title => "Users Module";
    public string Path => BasePath;
    public string SwaggerGroup => BasePath;
    public string Version => "v1";
}