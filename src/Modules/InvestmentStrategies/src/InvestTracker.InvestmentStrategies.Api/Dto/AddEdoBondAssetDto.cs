namespace InvestTracker.InvestmentStrategies.Api.Dto;

internal record AddEdoBondAssetDto(int Volume, DateOnly PurchaseDate, decimal FirstYearInterestRate, decimal Margin, string Note);