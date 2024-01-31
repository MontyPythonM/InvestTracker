using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Clients;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Entities;
using InvestTracker.InvestmentStrategies.Infrastructure.Options;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.BackgroundTasks;

internal sealed class UpdateInflationRatesTask : BackgroundService
{
    private readonly IInflationRateApiClient _inflationRateApiClient;
    private readonly InflationRateApiOptions _options;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<UpdateInflationRatesTask> _logger;
    private readonly ITimeProvider _timeProvider;
    
    public UpdateInflationRatesTask(IInflationRateApiClient inflationRateApiClient, InflationRateApiOptions options, 
        IServiceProvider serviceProvider, ILogger<UpdateInflationRatesTask> logger, ITimeProvider timeProvider)
    {
        _inflationRateApiClient = inflationRateApiClient;
        _options = options;
        _serviceProvider = serviceProvider;
        _logger = logger;
        _timeProvider = timeProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_options.Enabled is false)
        {
            return;
        }
        
        using var timer = new PeriodicTimer(TimeSpan.FromDays(_options.DurationDays));
        var errorsNumber = 0;
        
        do
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                await using var dbContext = scope.ServiceProvider.GetRequiredService<InvestmentStrategiesDbContext>();

                var lastInflationRate = await dbContext.InflationRates
                    .OrderBy(rate => rate.MonthlyDate)
                    .LastOrDefaultAsync(stoppingToken);

                var lastInflationRateDate = lastInflationRate?.MonthlyDate ?? new DateOnly(_options.UpdateMissingFromYear, 01, 01);
                
                var missingMonths = GetMissingMonths(lastInflationRateDate, _timeProvider.CurrentDate());
                var inflationRates = new List<InflationRateEntity>();
                
                foreach (var month in missingMonths)
                {
                    var inflationRate = await _inflationRateApiClient.GetInflationRateAsync(month, stoppingToken);
                    if (inflationRate is null)
                    {
                        continue;
                    }

                    inflationRates.Add(inflationRate);
                }
      
                await dbContext.InflationRates.AddRangeAsync(inflationRates, stoppingToken);
                await dbContext.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                var message = $"Background job named '{nameof(UpdateInflationRatesTask)}' failed. Message: {ex.Message}, details: {ex}";
                _logger.LogError(message);

                errorsNumber++;
                if (errorsNumber > _options.MaxErrorsNumber)
                {
                    _logger.LogCritical(message);
                    break;
                }
            }
        } while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken));
    }

    private static IEnumerable<MonthlyDate> GetMissingMonths(DateOnly lastInflationRateDate, DateOnly now)
    {
        var missingMonths = new List<MonthlyDate>();
        var lastMonthDate = new MonthlyDate(lastInflationRateDate);
        var currentMonthDate = new MonthlyDate(now);

        for (var year = lastMonthDate.Year; year <= currentMonthDate.Year; year++)
        {
            if (year != lastMonthDate.Year)
            {
                for (var month = 1; month <= 12; month++)
                {
                    if (currentMonthDate.Month == month && currentMonthDate.Year == year)
                    {
                        return missingMonths;
                    }
                    
                    missingMonths.Add(new MonthlyDate(year, month));
                }
            }
            
            if (year == lastMonthDate.Year)
            {
                for (var month = lastMonthDate.Month + 1; month <= 12; month++)
                {
                    missingMonths.Add(new MonthlyDate(year, month));
                }
            }
        }
        
        return missingMonths;
    }
}