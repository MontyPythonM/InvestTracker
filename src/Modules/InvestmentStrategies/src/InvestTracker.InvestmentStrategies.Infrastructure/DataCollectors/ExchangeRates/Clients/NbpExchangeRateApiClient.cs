using System.Net;
using System.Net.Http.Json;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients.Responses;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients.Responses.GetAllExchangeRates;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients.Responses.GetConcreteCurrencyExchangeRates;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Entities;
using InvestTracker.InvestmentStrategies.Infrastructure.Exceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.Options;
using InvestTracker.Shared.Abstractions.DDD.Exceptions;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.Extensions.Logging;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.Clients;

internal sealed class NbpExchangeRateApiClient : IExchangeRateApiClient
{
    private const string DateFormat = "yyyy-MM-dd";
    private readonly HttpClient _httpClient;
    private readonly ITimeProvider _timeProvider;
    private readonly ExchangeRateApiOptions _exchangeRateApiOptions;
    private readonly ILogger<NbpExchangeRateApiClient> _logger;

    public NbpExchangeRateApiClient(HttpClient httpClient, ITimeProvider timeProvider, ExchangeRateApiOptions exchangeRateApiOptions, 
        ILogger<NbpExchangeRateApiClient> logger)
    {
        _httpClient = httpClient;
        _timeProvider = timeProvider;
        _exchangeRateApiOptions = exchangeRateApiOptions;
        _logger = logger;
    }

    public async Task<IEnumerable<ExchangeRateEntity>> GetConcreteCurrencyAsync(Currency currency, DateRange dateRange, 
        CancellationToken token = default)
    {
        try
        {
            var daysLimitPerRequest = _exchangeRateApiOptions.GetDaysRequestLimit;

            if (dateRange.IsDaysLimitExceed(daysLimitPerRequest))
            {
                throw new MaxDaysLimitExceedException(daysLimitPerRequest);
            }

            var route = @$"rates/A/{currency.Value}/{dateRange.From.ToString(DateFormat)}/{dateRange.To.ToString(DateFormat)}";
            var responseResult = await _httpClient.GetFromJsonAsync<ConcreteExchangeRatesResponse>(route, token);

            return responseResult is not null
                ? responseResult.MapToExchangeRateEntities(_timeProvider.Current())
                : new List<ExchangeRateEntity>();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogInformation($"No results found. Status: {ex.StatusCode}, message: {ex.Message}.");
                return new List<ExchangeRateEntity>();
            }

            var errorMessage = $"Unhandled error from NbpExchangeRateApiClient GetAllAsync: {ex.Message}. Details: {ex}";
            _logger.LogError(errorMessage);
            throw new ExchangeRateApiClientException(errorMessage);
        }
    }

    public async Task<IEnumerable<ExchangeRateEntity>> GetAllAsync(DateRange dateRange, CancellationToken token = default)
    {
        try
        {
            var daysLimitPerRequest = _exchangeRateApiOptions.GetAllDaysRequestLimit;

            if (dateRange.IsDaysLimitExceed(daysLimitPerRequest))
            {
                throw new MaxDaysLimitExceedException(daysLimitPerRequest);
            }

            var route = @$"tables/A/{dateRange.From.ToString(DateFormat)}/{dateRange.To.ToString(DateFormat)}";
            var responseResults = await _httpClient.GetFromJsonAsync<List<ExchangeRateResponse>>(route, token);

            return responseResults is not null
                ? responseResults.MapToExchangeRateEntities(_timeProvider.Current())
                : new List<ExchangeRateEntity>();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogInformation($"No results found. Status: {ex.StatusCode}, message: {ex.Message}.");
                return new List<ExchangeRateEntity>();
            }

            var errorMessage = $"Unhandled error from NbpExchangeRate API: {ex.Message}. Details: {ex}";
            _logger.LogError(errorMessage);
            throw new ExchangeRateApiClientException(errorMessage);
        }
    }
}