using InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Extensions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Types;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;

public abstract class TreasuryBond : FinancialAsset
{
    public abstract int InvestmentDurationYears { get; }
    public abstract int NominalUnitValue { get; }
    
    public Volume CalculateVolume(Amount amount) => (int)amount / NominalUnitValue;

    protected decimal GetInvestmentPeriodCompletion(DateOnly calculationDate, DateOnly purchaseDate, DateOnly redemptionDate)
    {
        const decimal completedYear = 1;

        if (redemptionDate <= calculationDate)
        {
            return completedYear;
        }

        if (calculationDate < purchaseDate)
        {
            throw new DateOutOfInvestmentPeriodRangeException(calculationDate);
        }

        var calculationPeriod = purchaseDate
            .GetYearlyRanges((uint)InvestmentDurationYears)
            .Single(period => period.From <= calculationDate && period.To >= calculationDate);
        
        var calculationPeriodTotalDays = calculationPeriod.GetDaysNumber();
        var calculationDateDayOfPeriod = (calculationDate.ToDateTime() - calculationPeriod.From.ToDateTime()).TotalDays;
        
        return (decimal) calculationDateDayOfPeriod / calculationPeriodTotalDays;
    }
    
    protected static Dictionary<int, GroupedInflationRate> GroupInflationRatesIntoInvestmentYears(
        ChronologicalInflationRates chronologicalInflationRates, DateOnly calculateDate, DateOnly purchaseDate, int groupSize = 12)
    {
        var inflationRatesPerPeriods = new Dictionary<int, GroupedInflationRate>();
        var investmentYear = 1;
        var reducedInflationRates = chronologicalInflationRates.ReduceToDateRange(purchaseDate, calculateDate);

        for (var i = 0; i < reducedInflationRates.InflationRates.Count(); i += groupSize)
        {
            var newPeriod = new GroupedInflationRate();
            var groupedRates = reducedInflationRates.InflationRates
                .Skip(i)
                .Take(groupSize)
                .ToList();
            
            var lastMonth = groupedRates.Last();
            var isNotCompletedLastMonth = lastMonth.MonthlyDate.Year == calculateDate.Year && 
                                          lastMonth.MonthlyDate.Month == calculateDate.Month;
            
            if (groupedRates.Count == groupSize && !isNotCompletedLastMonth)
            {
                newPeriod.IsPeriodCompleted = true;
            }

            newPeriod.InflationRates = groupedRates;
            inflationRatesPerPeriods.Add(investmentYear, newPeriod);
            investmentYear++;
        }

        if (inflationRatesPerPeriods.Select(x => x.Value).Count(x => x.IsPeriodCompleted is false) > 1)
        {
            throw new MoreThanOneIncompletePeriodInGroupedInflationRatesException();
        }

        return inflationRatesPerPeriods;
    }
    
    protected static bool AreIncludedInInvestmentPeriod(ChronologicalInflationRates chronologicalInflationRates, DateOnly purchaseDate, DateOnly redemptionDate)
    {
        if (!chronologicalInflationRates.InflationRates.Any())
        {
            return true;
        }
        
        var firstRate = chronologicalInflationRates.InflationRates.First();
        var lastRate = chronologicalInflationRates.InflationRates.Last();

        var isFirstRateEqualsPurchaseDate = purchaseDate.Month == firstRate.MonthlyDate.Month && purchaseDate.Year == firstRate.MonthlyDate.Year;
        
        var isLastRateNotExceedRedemptionDate = 
            (redemptionDate.Month >= lastRate.MonthlyDate.Month && redemptionDate.Year == lastRate.MonthlyDate.Year) 
            || redemptionDate.Year > lastRate.MonthlyDate.Year;
        
        return isFirstRateEqualsPurchaseDate && isLastRateNotExceedRedemptionDate;
    }
    
    protected static InflationRate GetPreviousPeriodInflationRate(ChronologicalInflationRates chronologicalInflationRates, 
        int period, DateOnly purchaseDate)
    {
        var previousPeriodDate = new MonthlyDate(new DateOnly(purchaseDate.Year, purchaseDate.Month, 01)
            .AddYears(period - 1)
            .AddMonths(-2));
        
        var rate = chronologicalInflationRates.InflationRates.SingleOrDefault(rate => rate.MonthlyDate == previousPeriodDate);

        if (rate is null)
        {
            throw new InflationRateNotFoundException(previousPeriodDate);
        }
        
        return rate;
    }
    
    protected sealed class GroupedInflationRate
    {
        public IEnumerable<InflationRate> InflationRates { get; set; } = new List<InflationRate>();
        public bool IsPeriodCompleted { get; set; }
    }
}