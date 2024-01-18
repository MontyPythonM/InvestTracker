using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto.Charts;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.Shared.Abstractions.Types;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Services.Charts;

internal sealed class ChartService : IChartService
{
    public CashChart CalculateCashChart(IEnumerable<ExchangeRate> exchangeRates, IEnumerable<Transaction> transactions)
    {
        var chartPoints = new List<ChartValue<DateOnly, decimal>>();
        var orderedTransactions = transactions.OrderBy(t => t.TransactionDate).ToList();
        var orderedExchangeRates = exchangeRates.DistinctBy(rate => new { rate.Date, rate.To }).OrderBy(e => e.Date).ToList();
        
        foreach (var rate in orderedExchangeRates)
        {
            var amount = GetLastTransactionAmount(orderedTransactions, rate.Date);
            chartPoints.Add(new ChartValue<DateOnly, decimal>
            {
                X = rate.Date, 
                Y = amount.Value * rate.Value
            });
        }

        return new CashChart(orderedExchangeRates.First().To, chartPoints);
    }

    private static Amount GetLastTransactionAmount(IReadOnlyCollection<Transaction> transactions, DateOnly date)
    {
        if (!transactions.Any() || transactions.First().TransactionDate.ToDateOnly() > date)
        {
            return 0;
        }
        
        var existingTransactions = transactions
            .Where(t => t.TransactionDate.ToDateOnly() <= date)
            .ToList();

        return existingTransactions.OfType<IncomingTransaction>().Sum(x => x.Amount) -
               existingTransactions.OfType<OutgoingTransaction>().Sum(x => x.Amount);
    }
}