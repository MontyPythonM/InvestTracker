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

builder.Host.ConfigureModules();
builder.Services
    .AddSharedInfrastructure(assemblies)
    .AddOffersModule()
    .AddCalculatorsModule()
    .AddNotificationsModule()
    .AddExportsModule()
    .AddUsersModule()
    .AddInvestmentStrategiesModule();

var app = builder.Build();

app.UseSharedInfrastructure();
app.Run();