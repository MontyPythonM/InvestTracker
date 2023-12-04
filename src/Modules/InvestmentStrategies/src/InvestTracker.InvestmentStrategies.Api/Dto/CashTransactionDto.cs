namespace InvestTracker.InvestmentStrategies.Api.Dto;

public record CashTransactionDto(decimal Amount, DateTime TransactionDate, string Note);