namespace InvestTracker.InvestmentStrategies.Api.Dto;

internal record AddCoiAssetDto(int Volume, DateOnly PurchaseDate, decimal FirstYearInterestRate, decimal Margin, string Note);