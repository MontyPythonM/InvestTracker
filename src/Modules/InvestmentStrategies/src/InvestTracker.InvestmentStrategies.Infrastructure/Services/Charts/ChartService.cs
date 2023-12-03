using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.Shared.Abstractions.Types;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Services.Charts;

internal sealed class ChartService : IChartService
{
    public IEnumerable<CashChartValue> CalculateCashChart(IEnumerable<ExchangeRate> exchangeRates, 
        IEnumerable<AmountTransaction> transactions, Currency fromCurrency, Currency toCurrency)
    {
        var chartPoints = new List<CashChartValue>();
        var orderedTransactions = transactions.OrderBy(t => t.TransactionDate).ToList();
        var orderedExchangeRates = exchangeRates.DistinctBy(rate => new { rate.Date, rate.To }).OrderBy(e => e.Date).ToList();
        
        foreach (var rate in orderedExchangeRates)
        {
            var amount = GetLastTransactionAmount(orderedTransactions, rate.Date);
            chartPoints.Add(new CashChartValue(rate.Date, rate.To, amount.Value * rate.Value));
        }

        return chartPoints;
    }

    private static Amount GetLastTransactionAmount(IReadOnlyCollection<AmountTransaction> transactions, DateOnly date)
    {
        if (!transactions.Any() || transactions.First().TransactionDate.ToDateOnly() > date)
        {
            return 0;
        }
        
        var lastExistingTransaction = transactions.Last(t => t.TransactionDate.ToDateOnly() <= date);
        return lastExistingTransaction.Amount;
    }
}