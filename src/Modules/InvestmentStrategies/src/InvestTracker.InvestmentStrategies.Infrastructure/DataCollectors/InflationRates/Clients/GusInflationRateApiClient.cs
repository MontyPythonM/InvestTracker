using System.Net;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Consts;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Clients.Enums;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Clients.Responses;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Entities;
using InvestTracker.InvestmentStrategies.Infrastructure.Exceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.Options;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Clients;

internal sealed class GusInflationRateApiClient : IInflationRateApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GusInflationRateApiClient> _logger;
    private readonly ITimeProvider _timeProvider;
    private readonly InflationRateApiOptions _inflationRateApiOptions;
    
    public GusInflationRateApiClient(HttpClient httpClient, ILogger<GusInflationRateApiClient> logger, 
        ITimeProvider timeProvider, InflationRateApiOptions inflationRateApiOptions)
    {
        _httpClient = httpClient;
        _logger = logger;
        _timeProvider = timeProvider;
        _inflationRateApiOptions = inflationRateApiOptions;
    }
    
    public async Task<InflationRateEntity?> GetInflationRateAsync(MonthlyDate monthlyDate, CancellationToken token = default)
    {
        try
        {
            var requestUri = GetRequestUri(monthlyDate);

            var responseBody = await _httpClient.GetStringAsync(requestUri, token);
            var response = JsonConvert.DeserializeObject<InflationRateResponse>(responseBody);

            if (response is null)
            {
                throw new InflationRateApiClientException("Response is null");
            }

            var responseRate = GetInflationRateResponseDetails(response);

            if (responseRate is null)
            {
                throw new InflationRateNotFoundException(monthlyDate);
            }
            
            var metadata = $"source: 'API Client', selector: '{_inflationRateApiOptions.SelectorName}'";
            
            return new InflationRateEntity(Guid.NewGuid(), Currencies.PLN, monthlyDate, 
                _timeProvider.Current(), (responseRate.Value - 100) / 100, metadata);
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            var errorMessage = $"Unhandled error from GusInflationRate API: {ex.Message}. Details: {ex}";
            _logger.LogError(errorMessage);
            throw new InflationRateApiClientException($"Message: {errorMessage}");
        }
    }

    private string GetRequestUri(MonthlyDate monthlyDate)
    {
        var recordsNumber = _inflationRateApiOptions.RecordsNumber;
        var periodId = GusApiPeriods.GetByMonth(monthlyDate.Month);
        
        return $"variable/variable-data-section?id-zmienna=305&id-przekroj=736&id-rok={monthlyDate.Year}&id-okres={periodId.PeriodId}&ile-na-stronie={recordsNumber}&numer-strony=0";
    }

    private InflationRateResponseDetails? GetInflationRateResponseDetails(InflationRateResponse response)
    {
        return response.Details
            .SingleOrDefault(rate => rate.PositionId1 == _inflationRateApiOptions.PositionId1 &&
                                     rate.PositionId2 == _inflationRateApiOptions.PositionId2 &&
                                     rate.PositionId3 == _inflationRateApiOptions.PositionId3 &&
                                     rate.ReferenceMeasure == _inflationRateApiOptions.ReferenceMeasure);
    }
}