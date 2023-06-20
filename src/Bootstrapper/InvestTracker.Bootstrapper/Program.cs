using InvestTracker.Calculators.Api;
using InvestTracker.Exports.Api;
using InvestTracker.InvestmentStrategies.Api;
using InvestTracker.Notifications.Api;
using InvestTracker.Offers.Api;
using InvestTracker.Shared.Infrastructure;
using InvestTracker.Users.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSharedInfrastructure()
    .AddOffersModule()
    .AddCalculatorsModule()
    .AddNotificationsModule()
    .AddExportsModule()
    .AddUsersModule()
    .AddInvestmentStrategiesModule();

var app = builder.Build();

app.UseSharedInfrastructure();
app.Run();