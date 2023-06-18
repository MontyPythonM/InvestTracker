using InvestTracker.Offers.Api;
using InvestTracker.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSharedInfrastructure()
    .AddOffersModule();

var app = builder.Build();

app.UseSharedInfrastructure();
app.Run();