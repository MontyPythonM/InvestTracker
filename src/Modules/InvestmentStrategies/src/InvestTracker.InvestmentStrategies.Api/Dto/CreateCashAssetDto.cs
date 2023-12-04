namespace InvestTracker.InvestmentStrategies.Api.Dto;

internal record CreateCashAssetDto(string Currency, string Note, decimal? InitialAmount = null, DateTime? InitialDate = null);