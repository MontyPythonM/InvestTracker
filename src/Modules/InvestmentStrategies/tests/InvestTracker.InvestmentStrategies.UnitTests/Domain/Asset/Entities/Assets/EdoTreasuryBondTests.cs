using InvestTracker.InvestmentStrategies.Domain.Portfolios.Consts;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.Entities.FinancialAssets;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Types;
using Shouldly;
using Xunit;

namespace InvestTracker.InvestmentStrategies.UnitTests.Domain.Asset.Entities.Assets;

public class EdoTreasuryBondTests
{
    #region GetAmount tests
    
    /// <summary>
    /// Result should be equal 724,80 PLN for EDO0931 at 04.11.2023 (real case)
    /// </summary>
    [Fact]
    public void GetAmount_ShouldReturnCorrectAmountForRealEDO0931_WhenRealInitialDataAreUsed()
    {
        // arrange
        var volume = new Volume(6);
        var margin = new Margin(0.01M);
        var firstYearInterestRate = new InterestRate(0.017M);
        var purchaseDate = new DateTime(2021, 09, 07);
        var calculationDate = new DateTime(2023, 11, 04);

        var edoBond = CreateEdoTreasuryBond(volume, firstYearInterestRate, margin, purchaseDate);
        var inflationRates = GetRealInflationRatesForEdo0931();
        
        // act
        var result = edoBond.GetAmount(inflationRates, DateOnly.FromDateTime(calculationDate), volume, new DateTime(2025, 01, 01));
        
        // assert
        const decimal expectedAmount = 724.8M;
        var roundedResult = Math.Round(result, 1);
        
        roundedResult.ShouldBeEquivalentTo(expectedAmount);
    }
    
    [Theory]
    [MemberData(nameof(GetAmountTestData))]
    public void GetAmount_ShouldReturnCorrectAmount_WhenCalculateDateIsInInflationRatesDateRange(int volume, 
        decimal margin, decimal firstYearInterestRate, DateTime purchaseDate, DateTime calculateDate)
    {
        // arrange
        var edoBond = CreateEdoTreasuryBond(volume, firstYearInterestRate, margin, purchaseDate);
        var inflationRates = GetTenYearsChronologicalInflationRates(purchaseDate);
     
        var firstYearInflationRate = inflationRates.Values.Take(12).Select(rate => rate.Value).Average();
        var secondYearInflationRate = inflationRates.Values.Skip(12).Take(12).Select(rate => rate.Value).Average();
             
        var firstYearAmount = volume * 100 * (1 + firstYearInterestRate);
        var secondYearAmount = firstYearAmount * (1 + margin + firstYearInflationRate);
        var thirdYearAmount = secondYearAmount * (1 + margin + secondYearInflationRate);
        var thirdYearNonCompletedResult = (thirdYearAmount - secondYearAmount) * GetInvestmentYearCompletion(calculateDate, purchaseDate) + secondYearAmount;
             
        // act
        var result = edoBond.GetAmount(inflationRates, DateOnly.FromDateTime(calculateDate), volume, calculateDate);
             
        // assert
        var roundedResult = Math.Round(result, 4);
        var expectedAmount = Math.Round(thirdYearNonCompletedResult, 4);
        roundedResult.ShouldBeEquivalentTo(expectedAmount);
    }

    [Fact]
    public void GetAmount_ShouldReturnCorrectAmount_WhenPurchaseDateEqualsCalculateDate()
    {
        // arrange
        var volume = new Volume(10);
        var margin = new Margin(0.1M);
        var firstYearInterestRate = new InterestRate(0.1M);
        var purchaseDate = new DateTime(2020, 01, 01);
        var calculateDate = purchaseDate;
        var currentDate = new DateTime(2035, 01, 01);

        var edoBond = CreateEdoTreasuryBond(volume, firstYearInterestRate, margin, purchaseDate);
        var inflationRates = GetTenYearsChronologicalInflationRates(purchaseDate);

        var firstYearAmount = volume * 100 * (1 + firstYearInterestRate) * GetInvestmentYearCompletion(calculateDate, purchaseDate);
             
        // act
        var result = edoBond.GetAmount(inflationRates, DateOnly.FromDateTime(calculateDate), volume, currentDate);
             
        // assert
        result.Value.ShouldBeEquivalentTo(firstYearAmount);
    }
         
    [Fact]
    public void GetAmount_ShouldReturnCorrectAmount_WhenCalculatedDateIsLastDayOfInvestment()
    {
        // arrange
        const decimal constInflationRate = 0.025M;
        var volume = new Volume(1);
        var margin = new Margin(0.1M);
        var firstYearInterestRate = new InterestRate(0.015M);
        var purchaseDate = new DateTime(2020, 01, 10);
        var currentDate = new DateTime(2035, 01, 01);
        
        var edoBond = CreateEdoTreasuryBond(volume, firstYearInterestRate, margin, purchaseDate);
        var inflationRates = GetTenYearsChronologicalInflationRates(purchaseDate, false);
        
        var firstYearAmount = volume * 100 * (1 + firstYearInterestRate);
        decimal lastAmount = firstYearAmount;
        
        for (var year = 1; year < 10; year++)
        {
            lastAmount = lastAmount * (1 + margin + constInflationRate);
        }

        // act
        var result = edoBond.GetAmount(inflationRates, DateOnly.FromDateTime(currentDate), volume, currentDate);
        
        // assert
        result.Value.ShouldBeEquivalentTo(lastAmount);
    }
    
    #endregion
    
    #region Arrange
    
    public static IEnumerable<object[]> GetAmountTestData()
    {
        yield return new object[] { 10, 0.0125, 0.075, new DateTime(2023, 01, 01), new DateTime(2025, 01, 01) };
        yield return new object[] { 1, 0.0125, 0.075, new DateTime(2023, 05, 15), new DateTime(2025, 06, 26) };
        yield return new object[] { 100, 0.0125, 0.075, new DateTime(2023, 01, 01), new DateTime(2025, 12, 05) };
        yield return new object[] { 10000, 0.0125, 0.075, new DateTime(2023, 12, 01), new DateTime(2025, 12, 02) };
    }
    
    private static EdoTreasuryBond CreateEdoTreasuryBond(Volume volume, InterestRate firstYearInterestRate, Margin margin, DateTime purchaseDate)
        => new(
            "035B2CFD-B842-483B-ACFB-F5BFC45EE13A".ToGuid(),
            volume,
            purchaseDate,
            firstYearInterestRate,
            margin,
            new Note("Some short note about tested EDO bond")
        );
    
    private static decimal GetInvestmentYearCompletion(DateTime calculationDate, DateTime purchaseDate)
    {
        const int daysInYear = 365;
        var redemptionDate = purchaseDate.AddYears(10);
        var calculationPeriodFirstDay = new DateOnly(calculationDate.Year, redemptionDate.Month, redemptionDate.Day);
        var calculationDateDayNumber = calculationDate.DayOfYear - calculationPeriodFirstDay.DayOfYear;

        return (decimal)calculationDateDayNumber / daysInYear;
    }
    
    private static ChronologicalInflationRates GetTenYearsChronologicalInflationRates(DateTime purchaseDate, bool increaseInflationRate = true)
    {
        const int tenYearsMonths = 120;
        var year = purchaseDate.Year;
        var month = purchaseDate.Month;
        var inflationRates = new List<InflationRate>();
        var initialInflationRate = 0.025M;

        for (var i = 0; i < tenYearsMonths + 1; i++)
        {
            inflationRates.Add(new InflationRate(initialInflationRate, Currencies.PLN, year, month));

            if (increaseInflationRate)
            {
                initialInflationRate += 0.011M;
            }
            
            month++;
            
            if (month > 12)
            {
                year++;
                month = 1;
            }
        }

        return new ChronologicalInflationRates(inflationRates);
    }
    
    private static ChronologicalInflationRates GetRealInflationRatesForEdo0931()
    {
        const decimal averageInflationPeriod1 = 0.156M;
        const decimal averageInflationPeriod2 = 0.108M;
        const decimal averageInflationPeriod3 = 0.118M;
        
        return new ChronologicalInflationRates(new List<InflationRate>
        {
            new(averageInflationPeriod1, Currencies.PLN, 2021, 09),
            new(averageInflationPeriod1, Currencies.PLN, 2021, 10),
            new(averageInflationPeriod1, Currencies.PLN, 2021, 11),
            new(averageInflationPeriod1, Currencies.PLN, 2021, 12),
            new(averageInflationPeriod1, Currencies.PLN, 2022, 01),
            new(averageInflationPeriod1, Currencies.PLN, 2022, 02),
            new(averageInflationPeriod1, Currencies.PLN, 2022, 03),
            new(averageInflationPeriod1, Currencies.PLN, 2022, 04),
            new(averageInflationPeriod1, Currencies.PLN, 2022, 05),
            new(averageInflationPeriod1, Currencies.PLN, 2022, 06),
            new(averageInflationPeriod1, Currencies.PLN, 2022, 07),
            new(averageInflationPeriod1, Currencies.PLN, 2022, 08),
            
            new(averageInflationPeriod2, Currencies.PLN, 2022, 09),
            new(averageInflationPeriod2, Currencies.PLN, 2022, 10),
            new(averageInflationPeriod2, Currencies.PLN, 2022, 11),
            new(averageInflationPeriod2, Currencies.PLN, 2022, 12),
            new(averageInflationPeriod2, Currencies.PLN, 2023, 01),
            new(averageInflationPeriod2, Currencies.PLN, 2023, 02),
            new(averageInflationPeriod2, Currencies.PLN, 2023, 03),
            new(averageInflationPeriod2, Currencies.PLN, 2023, 04),
            new(averageInflationPeriod2, Currencies.PLN, 2023, 05),
            new(averageInflationPeriod2, Currencies.PLN, 2023, 06),
            new(averageInflationPeriod2, Currencies.PLN, 2023, 07),
            new(averageInflationPeriod2, Currencies.PLN, 2023, 08),

            new(averageInflationPeriod3, Currencies.PLN, 2023, 09),
            new(averageInflationPeriod3, Currencies.PLN, 2023, 10),
        });
    }
    
    #endregion
}