namespace InvestTracker.InvestmentStrategies.Api.Dto;

public record AddCashTransactionDto(decimal Amount, DateTime TransactionDate, string Note);