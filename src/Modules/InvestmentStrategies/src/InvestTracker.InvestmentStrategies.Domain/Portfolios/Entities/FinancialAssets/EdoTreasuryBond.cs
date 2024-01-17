using InvestTracker.InvestmentStrategies.Domain.Portfolios.Abstractions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Consts;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.Transactions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Exceptions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Extensions;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Auditable;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Types;

namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;

public class EdoTreasuryBond : TreasuryBond, IAuditable
{
    public string Symbol { get; private set; }
    public InterestRate FirstYearInterestRate { get; private set; }
    public Margin Margin { get; private set; }
    public DateOnly PurchaseDate { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
    public Guid? ModifiedBy { get; private set; }

    public sealed override int InvestmentDurationYears => 10;
    public sealed override int NominalUnitValue => 100;
    public override string AssetName => $"{Symbol} Treasury Bond";

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

        _transactions.Add(new IncomingTransaction(Guid.NewGuid(), volume * NominalUnitValue, purchaseDate.ToDateTime(), note));
    }

    public OutgoingTransaction EarlyRedeem(Volume redeemVolume, DateTime sellDate, Note note)
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
        
        var transaction = new OutgoingTransaction(Guid.NewGuid(), redeemVolume * NominalUnitValue, sellDate, note);
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

    public Volume GetCurrentVolume() 
        => GetNominalVolume() - _transactions.OfType<OutgoingTransaction>().Sum(t => (int)t.Amount / NominalUnitValue);

    public Volume GetVolume(DateOnly calculationDate)
    {
        if (calculationDate < PurchaseDate)
        {
            throw new DateOutOfInvestmentPeriodRangeException(calculationDate);
        }
        
        var outgoingTransactions = _transactions
            .OfType<OutgoingTransaction>()
            .Where(t => t.TransactionDate <= calculationDate.ToDateTime())
            .Sum(t => (int)t.Amount / NominalUnitValue);

        return GetNominalVolume() - outgoingTransactions;
    }

    public IEnumerable<InterestRate> CalculateInterestRates(ChronologicalInflationRates chronologicalInflationRates, DateOnly calculationDate)
    {
        var reducedInflationRates = chronologicalInflationRates
            .ReduceToDateRange(PurchaseDate, GetRedemptionDate())
            .SetZeroInflationRateForDeflation();
        
        if (!AreIncludedInInvestmentPeriod(reducedInflationRates, PurchaseDate, GetRedemptionDate()))
        {
            throw new InvalidInflationRatesYearsException(Id);
        }
        
        var calculatedYearCompletion = GetInvestmentPeriodCompletion(calculationDate, PurchaseDate, GetRedemptionDate());
        var inflationRatesPerPeriods = GroupInflationRatesIntoInvestmentYears(reducedInflationRates, calculationDate, PurchaseDate);

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

            var previousPeriodInflationRate = GetPreviousPeriodInflationRate(reducedInflationRates, period, PurchaseDate);

            if (inflationRatesPerPeriods[period].IsPeriodCompleted is false)
            {
                interestRates.Add((Margin + previousPeriodInflationRate.Value) * calculatedYearCompletion);
                continue;
            }
            
            interestRates.Add(Margin + previousPeriodInflationRate.Value);
        }

        return interestRates.Select(rate => new InterestRate(Math.Round(rate, 4)));
    }
    
    public DateOnly GetRedemptionDate() => PurchaseDate.AddYears(InvestmentDurationYears);
    
    public IEnumerable<DateRange> GetInvestmentPeriods() => GetInvestmentDateRange().DividePerYears(1);
    
    public DateRange GetInvestmentDateRange() => new(PurchaseDate, GetRedemptionDate());
    
    private Volume GetNominalVolume() => (int)_transactions.OfType<IncomingTransaction>().Single().Amount / NominalUnitValue;
}