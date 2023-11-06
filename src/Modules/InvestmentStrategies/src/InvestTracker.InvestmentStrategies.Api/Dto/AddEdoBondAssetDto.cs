namespace InvestTracker.InvestmentStrategies.Api.Dto;

internal record AddEdoBondAssetDto(int Volume, DateTime PurchaseDate, decimal FirstYearInterestRate, decimal Margin, string Note);