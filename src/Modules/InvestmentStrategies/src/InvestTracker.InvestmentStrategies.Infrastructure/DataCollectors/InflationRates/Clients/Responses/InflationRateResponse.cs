using Newtonsoft.Json;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Clients.Responses;

internal class InflationRateResponse
{
    [JsonProperty("page-number")]
    public int PageNumber { get; set; }
    
    [JsonProperty("page-size")]
    public int PageSize { get; set; }
    
    [JsonProperty("page-count")]
    public int PageCount { get; set; }

    [JsonProperty("data")]
    public IEnumerable<InflationRateResponseDetails> Details { get; set; } = new List<InflationRateResponseDetails>();
}