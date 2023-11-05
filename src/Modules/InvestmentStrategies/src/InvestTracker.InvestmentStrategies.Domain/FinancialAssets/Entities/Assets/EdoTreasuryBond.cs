using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Consts;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Dto;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities.Transactions.Volume;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects.Types;
using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Entities.Assets;

public sealed class EdoTreasuryBond : FinancialAsset
{
    private const int NominalUnitValue = 100;
    private const int InvestmentDurationYears = 10;

    public string Symbol { get; private set; }
    public InterestRate FirstYearInterestRate { get; private set; }
    public Margin Margin { get; private set; }
    public DateOnly RedemptionDate { get; private set; }
    public bool IsActive { get; private set; } = true;
    public IEnumerable<VolumeTransaction> Transactions
    {
        get => _transactions;
        set => _transactions = new HashSet<VolumeTransaction>(value);
    }
    
    private HashSet<VolumeTransaction> _transactions = new();

    private EdoTreasuryBond()
    {
    }
    
    public EdoTreasuryBond(FinancialAssetId id, PortfolioId portfolioId, Volume volume, DateTime purchaseDate, 
        InterestRate firstYearInterestRate, Margin margin, Note note, AssetTypeLimitDto assetTypeLimitDto) 
        : base(id, portfolioId, Currencies.PLN, note, assetTypeLimitDto)
    {
        Symbol = $"EDO{purchaseDate:MMyy}";
        FirstYearInterestRate = firstYearInterestRate;
        Margin = margin;
        RedemptionDate = DateOnly.FromDateTime(purchaseDate.AddYears(InvestmentDurationYears));
        
        var openingTransaction = new IncomingVolumeTransaction(Guid.NewGuid(), volume, purchaseDate, note);
        _transactions.Add(openingTransaction);
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
        IncrementVersion();
        
        return transaction;
    }

    public void PlannedRedeem(DateOnly today)
    {
        if (RedemptionDate > today)
        {
            throw new PlannedRedemptionTooEarlyException(RedemptionDate);
        }
        
        IsActive = false;
        IncrementVersion();
    }
    
    public Amount GetAmount(ChronologicalInflationRates chronologicalInflationRates, DateOnly calculationDate, Volume volume, DateTime now)
    {
        if (!IsInflationRatesCoverEntireInvestment(chronologicalInflationRates))
        {
            throw new InvalidInflationRatesYearsException(Id);
        }

        if (calculationDate > DateOnly.FromDateTime(now))
        {
            throw new EdoTreasuryBondAmountFromFutureException();
        }

        var calculateInterestRate = CalculateInterestRate(chronologicalInflationRates, calculationDate);

        return volume * NominalUnitValue * calculateInterestRate;
    }
    
    public Amount GetCurrentAmount(ChronologicalInflationRates chronologicalInflationRates, DateTime now) 
        => GetAmount(chronologicalInflationRates, DateOnly.FromDateTime(now), GetCurrentVolume(), now);

    public Volume GetCurrentVolume() 
        => GetNominalVolume() - _transactions.OfType<OutgoingVolumeTransaction>().Sum(t => t.Volume);

    private InterestRate CalculateInterestRate(ChronologicalInflationRates chronologicalInflationRates, DateOnly calculationDate)
    {
        var calculatedYearCompletion = GetInvestmentYearCompletion(calculationDate, RedemptionDate);
        
        var calculationPeriodInflationRates = chronologicalInflationRates.Values
            .Where(rate => rate.Year < calculationDate.Year || 
                           rate.Year == calculationDate.Year && rate.Month <= calculationDate.Month);
        
        var inflationRatesPerPeriods = GroupInflationRatesIntoInvestmentYears(
            new ChronologicalInflationRates(calculationPeriodInflationRates), calculationDate);

        var calculationPeriods = inflationRatesPerPeriods.Count > InvestmentDurationYears ? 
            InvestmentDurationYears : 
            inflationRatesPerPeriods.Count;
        
        decimal cumulativeInterestRate = 0;
        for (var period = 1; period <= calculationPeriods; period++)
        {
            if (period == 1)
            {
                if (calculationPeriods == 1)
                {
                    cumulativeInterestRate = (1 + FirstYearInterestRate) * calculatedYearCompletion;
                    break;
                }

                cumulativeInterestRate = (1 + FirstYearInterestRate);
                continue;
            }

            var averagePreviousPeriodInflation = inflationRatesPerPeriods[period - 1].InflationRates.Average(rate => rate.Value);

            if (inflationRatesPerPeriods[period].IsPeriodCompleted is false)
            {
                cumulativeInterestRate *= 1 + (Margin + averagePreviousPeriodInflation) * calculatedYearCompletion;
                continue;
            }

            cumulativeInterestRate *= (1 + Margin + averagePreviousPeriodInflation);
        }

        return cumulativeInterestRate;
    }
    
    private static decimal GetInvestmentYearCompletion(DateOnly calculationDate, DateOnly redemptionDate)
    {
        const int daysInYear = 365;
        
        if (redemptionDate <= calculationDate)
        {
            return 1;
        }
        
        var calculationPeriodFirstDay = new DateOnly(calculationDate.Year, redemptionDate.Month, redemptionDate.Day);
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
            var isNotCompletedLastMonth = lastMonth.Year == calculateDate.Year && lastMonth.Month == calculateDate.Month;
            
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
        => purchaseDate.Month == firstRate.Month && purchaseDate.Year == firstRate.Year;
    
    private bool IsLastRateNotExceedRedemptionDate(InflationRate lastRate)
        => (RedemptionDate.Month >= lastRate.Month && RedemptionDate.Year == lastRate.Year) || RedemptionDate.Year > lastRate.Year;

    private DateOnly GetPurchaseDate() 
        => DateOnly.FromDateTime(_transactions.OfType<IncomingVolumeTransaction>().Single().TransactionDate);

    private bool IsFirstInvestmentYear(DateOnly date) => GetPurchaseDate().AddYears(1) > date;

    private sealed class GroupedInflationRate
    {
        public IEnumerable<InflationRate> InflationRates { get; set; } = new List<InflationRate>();
        public bool IsPeriodCompleted { get; set; }
    }
}