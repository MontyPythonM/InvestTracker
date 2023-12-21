using InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Consts;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions.Volume;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Extensions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Auditable;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Types;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;

public class EdoTreasuryBond : IFinancialAsset, IAuditable
{
    private const int NominalUnitValue = 100;
    private const int InvestmentDurationYears = 10;

    public FinancialAssetId Id { get; private set; }
    public string Symbol { get; private set; }
    public InterestRate FirstYearInterestRate { get; private set; }
    public Margin Margin { get; private set; }
    public DateOnly RedemptionDate { get; private set; }
    public Currency Currency { get; private set; }
    public Note Note { get; private set; }
    public bool IsActive { get; private set; } = true;
    public PortfolioId PortfolioId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public Guid? ModifiedBy { get; private set; }
    public IEnumerable<VolumeTransaction> Transactions
    {
        get => _transactions;
        set => _transactions = new HashSet<VolumeTransaction>(value);
    }
    
    private HashSet<VolumeTransaction> _transactions = new();

    private EdoTreasuryBond()
    {
    }
    
    internal EdoTreasuryBond(FinancialAssetId id, Volume volume, DateTime purchaseDate, 
        InterestRate firstYearInterestRate, Margin margin, Note note)
    {
        var redemptionDate = purchaseDate.AddYears(InvestmentDurationYears);
        
        Id = id;
        Symbol = $"EDO{redemptionDate:MMyy}";
        FirstYearInterestRate = firstYearInterestRate;
        Margin = margin;
        RedemptionDate = redemptionDate.ToDateOnly();
        Currency = Currencies.PLN;
        Note = note;

        _transactions.Add(new IncomingVolumeTransaction(Guid.NewGuid(), volume, purchaseDate, note));
    }

    public OutgoingVolumeTransaction EarlyRedeem(Volume redeemVolume, DateTime sellDate, Note note)
    {
        var currentVolume = GetCurrentVolume();
        if (currentVolume < redeemVolume)
        {
            throw new InsufficientAssetVolumeException(Id);
        }
        
        if (currentVolume - redeemVolume == 0)
        {
            IsActive = false;
        }
        
        var transaction = new OutgoingVolumeTransaction(Guid.NewGuid(), redeemVolume, sellDate, note);
        _transactions.Add(transaction);
        
        return transaction;
    }

    public void PlannedRedeem(DateOnly today)
    {
        if (RedemptionDate > today)
        {
            throw new PlannedRedemptionTooEarlyException(RedemptionDate);
        }
        
        IsActive = false;
    }
    
    public Amount GetAmount(ChronologicalInflationRates chronologicalInflationRates, DateOnly calculationDate, Volume volume, DateTime now)
    {
        var reducedInflationRates = chronologicalInflationRates.ReduceToTimeScope(GetPurchaseDate(), RedemptionDate);
        
        if (!IsInflationRatesCoverEntireInvestment(reducedInflationRates))
        {
            throw new InvalidInflationRatesYearsException(Id);
        }

        if (calculationDate > now.ToDateOnly())
        {
            throw new EdoTreasuryBondAmountFromFutureException();
        }

        var cumulativeInterestRate = CalculateInterestRates(reducedInflationRates, calculationDate).GetCumulativeInterestRate();

        return Math.Round(volume * NominalUnitValue * cumulativeInterestRate, 2);
    }
    
    public Amount GetCurrentAmount(ChronologicalInflationRates chronologicalInflationRates, DateTime now) 
        => GetAmount(chronologicalInflationRates, DateOnly.FromDateTime(now), GetCurrentVolume(), now);

    public Volume GetCurrentVolume() 
        => GetNominalVolume() - _transactions.OfType<OutgoingVolumeTransaction>().Sum(t => t.Volume);

    public string GetAssetName() => $"{Symbol} Treasury Bond";

    private IEnumerable<InterestRate> CalculateInterestRates(ChronologicalInflationRates chronologicalInflationRates, DateOnly calculationDate)
    {
        var calculatedYearCompletion = GetYearPeriodCompletion(calculationDate, RedemptionDate);
        var inflationRatesPerPeriods = GroupInflationRatesIntoInvestmentYears(chronologicalInflationRates, calculationDate);

        var interestRates = new List<InterestRate>();
        for (var period = 1; period <= inflationRatesPerPeriods.Count; period++)
        {
            if (period == 1)
            {
                if (inflationRatesPerPeriods.Count == 1)
                {
                    interestRates.Add(FirstYearInterestRate * calculatedYearCompletion);
                    break;
                }

                interestRates.Add(FirstYearInterestRate);
                continue;
            }

            var previousPeriodInflationRate = GetPreviousPeriodInflationRate(chronologicalInflationRates, period);

            if (inflationRatesPerPeriods[period].IsPeriodCompleted is false)
            {
                interestRates.Add((Margin + previousPeriodInflationRate.Value) * calculatedYearCompletion);
                continue;
            }
            
            interestRates.Add(Margin + previousPeriodInflationRate.Value);
        }

        return interestRates;
    }
    
    private InflationRate GetPreviousPeriodInflationRate(ChronologicalInflationRates chronologicalInflationRates, int period)
    {
        var purchaseDate = GetPurchaseDate();
        var purchaseMonthlyDate = new MonthlyDate(purchaseDate.Year + period - 1, purchaseDate.Month - 2);

        return chronologicalInflationRates.Values.Single(rate => rate.MonthlyDate == purchaseMonthlyDate);
    }

    private static decimal GetYearPeriodCompletion(DateOnly calculationDate, DateOnly lastYearPeriodDate)
    {
        var daysInYear = calculationDate.GetDaysInYear();
        
        if (lastYearPeriodDate <= calculationDate)
        {
            return 1;
        }
        
        var calculationPeriodFirstDay = new DateOnly(calculationDate.Year, lastYearPeriodDate.Month, lastYearPeriodDate.Day);
        var calculationDateDayNumber = calculationDate.DayOfYear - calculationPeriodFirstDay.DayOfYear;
        
        return (decimal)calculationDateDayNumber / daysInYear;
    }

    private static Dictionary<int, GroupedInflationRate> GroupInflationRatesIntoInvestmentYears(
        ChronologicalInflationRates chronologicalInflationRates, DateOnly calculateDate, int groupSize = 12)
    {
        var inflationRatesPerPeriods = new Dictionary<int, GroupedInflationRate>();
        var investmentYear = 1;

        for (var i = 0; i < chronologicalInflationRates.Values.Count(); i += groupSize)
        {
            var newPeriod = new GroupedInflationRate();
            var groupedRates = chronologicalInflationRates.Values
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
    
    private Volume GetNominalVolume() 
        => _transactions.OfType<IncomingVolumeTransaction>().Single().Volume;
    
    private bool IsInflationRatesCoverEntireInvestment(ChronologicalInflationRates chronologicalInflationRates)
    {
        var purchaseDate = GetPurchaseDate();
        var firstRate = chronologicalInflationRates.Values.First();
        var lastRate = chronologicalInflationRates.Values.Last();

        return IsFirstRateEqualsPurchaseDate(purchaseDate, firstRate) && IsLastRateNotExceedRedemptionDate(lastRate);
    }
    
    private static bool IsFirstRateEqualsPurchaseDate(DateOnly purchaseDate, InflationRate firstRate)
        => purchaseDate.Month == firstRate.MonthlyDate.Month && purchaseDate.Year == firstRate.MonthlyDate.Year;
    
    private bool IsLastRateNotExceedRedemptionDate(InflationRate lastRate)
        => (RedemptionDate.Month >= lastRate.MonthlyDate.Month && RedemptionDate.Year == lastRate.MonthlyDate.Year) || 
           RedemptionDate.Year > lastRate.MonthlyDate.Year;

    private DateOnly GetPurchaseDate() 
        => _transactions.OfType<IncomingVolumeTransaction>().Single().TransactionDate.ToDateOnly();

    private bool IsFirstInvestmentYear(DateOnly date) => GetPurchaseDate().AddYears(1) > date;

    private sealed class GroupedInflationRate
    {
        public IEnumerable<InflationRate> InflationRates { get; set; } = new List<InflationRate>();
        public bool IsPeriodCompleted { get; set; }
    }
}