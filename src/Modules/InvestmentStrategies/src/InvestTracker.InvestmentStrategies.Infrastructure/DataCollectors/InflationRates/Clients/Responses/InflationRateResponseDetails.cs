using Newtonsoft.Json;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Clients.Responses;

internal class InflationRateResponseDetails
{
    [JsonProperty("rownumber")] 
    public int RowNumber { get; set; }
    
    [JsonProperty("id-zmienna")] 
    public int VariableId { get; set; }
    
    [JsonProperty("id-przekroj")] 
    public int SectionId { get; set; }
    
    [JsonProperty("id-pozycja-1")] 
    public int PositionId1 { get; set; }
    
    [JsonProperty("id-pozycja-2")] 
    public int PositionId2 { get; set; }
    
    [JsonProperty("id-pozycja-3")] 
    public int PositionId3 { get; set; }
    
    [JsonProperty("id-okres")] 
    public int PeriodId { get; set; }
    
    [JsonProperty("id-daty")] 
    public int Year { get; set; }
    
    [JsonProperty("wartosc")] 
    public decimal Value { get; set; }
    
    [JsonProperty("precyzja")] 
    public int Precision { get; set; } 
    
    [JsonProperty("id-sposob-prezentacji-miara")]
    public int ReferenceMeasure { get; set; } 
}