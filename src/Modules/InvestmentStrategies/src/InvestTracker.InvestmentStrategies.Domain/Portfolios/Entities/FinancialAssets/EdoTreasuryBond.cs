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
    private HashSet<VolumeTransaction> _transactions = new();

    public FinancialAssetId Id { get; private set; }
    public string Symbol { get; private set; }
    public InterestRate FirstYearInterestRate { get; private set; }
    public Margin Margin { get; private set; }
    public DateOnly PurchaseDate { get; set; }
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
    
    private EdoTreasuryBond()
    {
    }
    
    internal EdoTreasuryBond(FinancialAssetId id, Volume volume, DateOnly purchaseDate, 
        InterestRate firstYearInterestRate, Margin margin, Note note)
    {
        Id = id;
        Symbol = $"EDO{purchaseDate.AddYears(InvestmentDurationYears):MMyy}";
        PurchaseDate = purchaseDate;
        FirstYearInterestRate = firstYearInterestRate;
        Margin = margin;
        Currency = Currencies.PLN;
        Note = note;

        _transactions.Add(new IncomingVolumeTransaction(Guid.NewGuid(), volume, purchaseDate.ToDateTime(), note));
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
        var redemptionDate = GetRedemptionDate();
        if (redemptionDate > today)
        {
            throw new PlannedRedemptionTooEarlyException(redemptionDate);
        }
        
        IsActive = false;
    }
    
    public Amount GetAmount(ChronologicalInflationRates chronologicalInflationRates, DateOnly calculationDate)
    {
        if (calculationDate < PurchaseDate)
        {
            throw new DateOutOfInvestmentPeriodRangeException(calculationDate);
        }

        var volume = GetVolume(calculationDate);
        var cumulativeInterestRate = CalculateInterestRates(chronologicalInflationRates, calculationDate).GetCumulativeInterestRate();

        return Math.Round(volume * NominalUnitValue * cumulativeInterestRate, 2);
    }
    
    public Amount GetCurrentAmount(ChronologicalInflationRates chronologicalInflationRates, DateOnly now) 
        => GetAmount(chronologicalInflationRates, now);

    public Volume GetCurrentVolume()
        => GetNominalVolume() - _transactions.OfType<OutgoingVolumeTransaction>().Sum(t => t.Volume);

    public Volume GetVolume(DateOnly calculationDate)
    {
        if (calculationDate < PurchaseDate)
        {
            throw new DateOutOfInvestmentPeriodRangeException(calculationDate);
        }
        
        var outgoingTransactions = _transactions
            .OfType<OutgoingVolumeTransaction>()
            .Where(t => t.TransactionDate <= calculationDate.ToDateTime())
            .Sum(t => t.Volume);

        return GetNominalVolume() - outgoingTransactions;
    }

    public string GetAssetName() => $"{Symbol} Treasury Bond";

    public DateOnly GetRedemptionDate() => PurchaseDate.AddYears(InvestmentDurationYears);

    public IEnumerable<InterestRate> CalculateInterestRates(ChronologicalInflationRates chronologicalInflationRates, DateOnly calculationDate)
    {
        var reducedInflationRates = chronologicalInflationRates
            .ReduceToDateRange(PurchaseDate, GetRedemptionDate())
            .SetZeroInflationRateForDeflation();
        
        if (!AreIncludedInInvestmentPeriod(reducedInflationRates))
        {
            throw new InvalidInflationRatesYearsException(Id);
        }
        
        var calculatedYearCompletion = GetInvestmentPeriodCompletion(calculationDate);
        var inflationRatesPerPeriods = GroupInflationRatesIntoInvestmentYears(reducedInflationRates, calculationDate);

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

            var previousPeriodInflationRate = GetPreviousPeriodInflationRate(reducedInflationRates, period);

            if (inflationRatesPerPeriods[period].IsPeriodCompleted is false)
            {
                interestRates.Add((Margin + previousPeriodInflationRate.Value) * calculatedYearCompletion);
                continue;
            }
            
            interestRates.Add(Margin + previousPeriodInflationRate.Value);
        }

        return interestRates.Select(rate => new InterestRate(Math.Round(rate, 4)));
    }

    public IEnumerable<DateRange> GetInvestmentPeriods() =>  GetInvestmentDateRange().DividePerYears(1);

    public DateRange GetInvestmentDateRange() => new(PurchaseDate, GetRedemptionDate());
    
    private InflationRate GetPreviousPeriodInflationRate(ChronologicalInflationRates chronologicalInflationRates, int period)
    {
        var previousPeriodDate = new MonthlyDate(new DateOnly(PurchaseDate.Year, PurchaseDate.Month, 01)
            .AddYears(period - 1)
            .AddMonths(-2));
        
        var rate = chronologicalInflationRates.Values.SingleOrDefault(rate => rate.MonthlyDate == previousPeriodDate);

        if (rate is null)
        {
            throw new InflationRateNotFoundException(previousPeriodDate);
        }
        
        return rate;
    }

    private decimal GetInvestmentPeriodCompletion(DateOnly calculationDate)
    {
        const decimal completedYear = 1;
        var redemptionDate = GetRedemptionDate();

        if (redemptionDate <= calculationDate)
        {
            return completedYear;
        }

        if (calculationDate < PurchaseDate)
        {
            throw new DateOutOfInvestmentPeriodRangeException(calculationDate);
        }

        var calculationPeriod = PurchaseDate
            .GetYearlyRanges(InvestmentDurationYears)
            .Single(period => period.From <= calculationDate && period.To >= calculationDate);
        
        var calculationPeriodTotalDays = calculationPeriod.GetDaysNumber();
        var calculationDateDayOfPeriod = (calculationDate.ToDateTime() - calculationPeriod.From.ToDateTime()).TotalDays;
        
        return (decimal) calculationDateDayOfPeriod / calculationPeriodTotalDays;
    }

    private Dictionary<int, GroupedInflationRate> GroupInflationRatesIntoInvestmentYears(
        ChronologicalInflationRates chronologicalInflationRates, DateOnly calculateDate, int groupSize = 12)
    {
        var inflationRatesPerPeriods = new Dictionary<int, GroupedInflationRate>();
        var investmentYear = 1;
        var reducedInflationRates = chronologicalInflationRates.ReduceToDateRange(PurchaseDate, calculateDate);

        for (var i = 0; i < reducedInflationRates.Values.Count(); i += groupSize)
        {
            var newPeriod = new GroupedInflationRate();
            var groupedRates = reducedInflationRates.Values
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
    
    private bool AreIncludedInInvestmentPeriod(ChronologicalInflationRates chronologicalInflationRates)
    {
        var redemptionDate = GetRedemptionDate();
        if (!chronologicalInflationRates.Values.Any())
        {
            return true;
        }
        
        var firstRate = chronologicalInflationRates.Values.First();
        var lastRate = chronologicalInflationRates.Values.Last();

        var isFirstRateEqualsPurchaseDate = PurchaseDate.Month == firstRate.MonthlyDate.Month && PurchaseDate.Year == firstRate.MonthlyDate.Year;
        
        var isLastRateNotExceedRedemptionDate = 
            (redemptionDate.Month >= lastRate.MonthlyDate.Month && redemptionDate.Year == lastRate.MonthlyDate.Year) 
            || redemptionDate.Year > lastRate.MonthlyDate.Year;
        
        return isFirstRateEqualsPurchaseDate && isLastRateNotExceedRedemptionDate;
    }
    
    private bool IsFirstInvestmentYear(DateOnly date) => PurchaseDate.AddYears(1) > date;

    private sealed class GroupedInflationRate
    {
        public IEnumerable<InflationRate> InflationRates { get; set; } = new List<InflationRate>();
        public bool IsPeriodCompleted { get; set; }
    }
}