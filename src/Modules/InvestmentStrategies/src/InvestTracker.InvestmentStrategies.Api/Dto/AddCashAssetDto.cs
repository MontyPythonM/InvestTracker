namespace InvestTracker.InvestmentStrategies.Api.Dto;

internal record AddCashAssetDto(string Currency, string Note, decimal? InitialAmount);