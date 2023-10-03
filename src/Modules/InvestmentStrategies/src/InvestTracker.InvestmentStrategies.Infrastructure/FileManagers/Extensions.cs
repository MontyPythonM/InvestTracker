using InvestTracker.InvestmentStrategies.Infrastructure.FileManagers.Csv;
using Microsoft.Extensions.DependencyInjection;

namespace InvestTracker.InvestmentStrategies.Infrastructure.FileManagers;

internal static class Extensions
{
    internal static IServiceCollection AddFileManagers(this IServiceCollection services)
    {
        services.AddSingleton<ICsvReader, CsvReader>();
        return services;
    }
}