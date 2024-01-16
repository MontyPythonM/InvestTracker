namespace InvestTracker.InvestmentStrategies.Api.Dto;

internal record AddEdoAssetDto(int Volume, DateOnly PurchaseDate, decimal FirstYearInterestRate, decimal Margin, string Note);