using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Services.Charts;

public interface IChartService
{
    CashChart CalculateCashChart(IEnumerable<ExchangeRate> exchangeRates, IEnumerable<AmountTransaction> transactions);
}