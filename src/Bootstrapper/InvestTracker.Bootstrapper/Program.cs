using InvestTracker.Bootstrapper;
using InvestTracker.Calculators.Api;
using InvestTracker.Exports.Api;
using InvestTracker.InvestmentStrategies.Api;
using InvestTracker.Notifications.Api;
using InvestTracker.Offers.Api;
using InvestTracker.Shared.Infrastructure;
using InvestTracker.Shared.Infrastructure.Modules;
using InvestTracker.Users.Api;

var builder = WebApplication.CreateBuilder(args);
var assemblies = ModuleLoader.LoadAssemblies();
var modules = ModuleLoader.LoadModules(assemblies);

builder.Host.ConfigureModuleAppSettings();

builder.Services
    .AddSharedInfrastructure(assemblies, modules)
    .AddOffersModule()
    .AddCalculatorsModule()
    .AddNotificationsModule()
    .AddExportsModule()
    .AddUsersModule()
    .AddInvestmentStrategiesModule();

var app = builder.Build();

app.UseSharedInfrastructure(modules);
app.Run();