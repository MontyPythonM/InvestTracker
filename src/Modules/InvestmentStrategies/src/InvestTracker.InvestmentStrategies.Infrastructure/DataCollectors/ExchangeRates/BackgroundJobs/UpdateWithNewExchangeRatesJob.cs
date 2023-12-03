﻿using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients;
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
    private readonly ExchangeRateOptions _exchangeRateOptions;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<UpdateWithNewExchangeRatesJob> _logger;
    
    public UpdateWithNewExchangeRatesJob(IExchangeRateApiClient exchangeRateApiClient, ITimeProvider timeProvider, 
        ExchangeRateOptions exchangeRateOptions, IServiceProvider serviceProvider, ILogger<UpdateWithNewExchangeRatesJob> logger)
    {
        _exchangeRateApiClient = exchangeRateApiClient;
        _timeProvider = timeProvider;
        _exchangeRateOptions = exchangeRateOptions;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_exchangeRateOptions.Enabled is false)
        {
            return;
        }
        
        using var timer = new PeriodicTimer(TimeSpan.FromHours(_exchangeRateOptions.DurationHours));
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
                var message = $"Background job named 'UpdateWithNewExchangeRatesJob' failed. Message: {ex.Message}, details: {ex}";
                _logger.LogError(message);

                errorsNumber++;
                if (errorsNumber > _exchangeRateOptions.MaxErrorsNumber)
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
            .OrderBy(rate => rate.Date)
            .LastOrDefaultAsync(token);

        var lastExchangeRate = lastPersistedExchangeRate is null
            ? _exchangeRateOptions.UpdateMissingFromDate
            : lastPersistedExchangeRate.Date.AddDays(1);
        
        var dateRange = new DateRange(lastExchangeRate, _timeProvider.CurrentDate());
        var dividedDateRanges = dateRange.Divide(_exchangeRateOptions.GetAllDaysRequestLimit);
        
        foreach (var range in dividedDateRanges)
        {
            var results = await _exchangeRateApiClient.GetAllAsync(range, token);
            exchangeRates.AddRange(results);
        }

        return exchangeRates;
    }
}