namespace InvestTracker.InvestmentStrategies.Api.Dto;

public record GetCashChartDto(string DisplayInCurrency, DateOnly DateFrom, DateOnly DateTo);