using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Entities;
using InvestTracker.InvestmentStrategies.Infrastructure.Options;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.BackgroundJobs;

internal sealed class UpdateWithNewExchangeRatesJob : BackgroundService
{
    private readonly IExchangeRateApiClient _exchangeRateApiClient;
    private readonly ITimeProvider _timeProvider;
    private readonly ExchangeRateApiOptions _exchangeRateApiOptions;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<UpdateWithNewExchangeRatesJob> _logger;
    
    public UpdateWithNewExchangeRatesJob(IExchangeRateApiClient exchangeRateApiClient, ITimeProvider timeProvider, 
        ExchangeRateApiOptions exchangeRateApiOptions, IServiceProvider serviceProvider, ILogger<UpdateWithNewExchangeRatesJob> logger)
    {
        _exchangeRateApiClient = exchangeRateApiClient;
        _timeProvider = timeProvider;
        _exchangeRateApiOptions = exchangeRateApiOptions;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_exchangeRateApiOptions.Enabled is false)
        {
            return;
        }
        
        using var timer = new PeriodicTimer(TimeSpan.FromHours(_exchangeRateApiOptions.DurationHours));
        var errorsNumber = 0;
        
        do
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                await using var dbContext = scope.ServiceProvider.GetRequiredService<InvestmentStrategiesDbContext>();

                var exchangeRates = await GetNewExchangeRates(dbContext, stoppingToken);

                await dbContext.ExchangeRates.AddRangeAsync(exchangeRates, stoppingToken);
                await dbContext.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                var message = $"Background job named '{nameof(UpdateWithNewExchangeRatesJob)}' failed. Message: {ex.Message}, details: {ex}";
                _logger.LogError(message);

                errorsNumber++;
                if (errorsNumber > _exchangeRateApiOptions.MaxErrorsNumber)
                {
                    _logger.LogCritical(message);
                    break;
                }
            }
        } while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken));
    }

    private async Task<IEnumerable<ExchangeRateEntity>> GetNewExchangeRates(InvestmentStrategiesDbContext dbContext, CancellationToken token)
    {
        var exchangeRates = new List<ExchangeRateEntity>();
        var lastPersistedExchangeRate = await dbContext.ExchangeRates
            .AsNoTracking()
            .OrderBy(rate => rate.Date)
            .LastOrDefaultAsync(token);

        var lastExchangeRateDate = lastPersistedExchangeRate?.Date ?? _exchangeRateApiOptions.UpdateMissingFromDate;

        if (lastExchangeRateDate >= _timeProvider.CurrentDate())
        {
            return exchangeRates;
        }

        var dateRange = new DateRange(lastExchangeRateDate.AddDays(1), _timeProvider.CurrentDate());
        var dividedDateRanges = dateRange.DividePerDays(_exchangeRateApiOptions.GetAllDaysRequestLimit);
        
        foreach (var range in dividedDateRanges)
        {
            var results = await _exchangeRateApiClient.GetAllAsync(range, token);
            exchangeRates.AddRange(results);
        }

        var existingExchangeRates = await dbContext.ExchangeRates
            .AsNoTracking()
            .Where(rate => rate.Date >= dateRange.From && rate.Date <= dateRange.To)
            .Select(rate => rate.Date)
            .ToListAsync(token);
        
        return exchangeRates.Where(rate => !existingExchangeRates.Contains(rate.Date));
    }
}