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
    /// Result should be equal 724,80 PLN for EDO0931 at 04.11.2023 (real case).
    /// The inaccuracy results from the fact that the National Bank of Poland publishes estimated inflation rates (slightly different from the final one, known later).
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
        var result = edoBond.GetAmount(inflationRates, DateOnly.FromDateTime(calculationDate), volume,
            new DateTime(2025, 01, 01));

        // assert
        const decimal expectedAmount = 724.8M;
        var roundedResult = Math.Round(result, 1);

        roundedResult.ShouldBeEquivalentTo(expectedAmount);
    }

    /// <summary>
    /// Result should be around 4363,90 PLN / 4356,08 PLN for EDO0931 at 04.01.2024 (real case).
    /// The inaccuracy results from the fact that the National Bank of Poland publishes estimated inflation rates (slightly different from the final one, known later).
    /// </summary>
    [Fact]
    public void GetAmount_ShouldReturnCorrectAmountForRealEDO0331_WhenRealInitialDataAreUsed()
    {
        // arrange
        var volume = new Volume(34);
        var margin = new Margin(0.01M);
        var firstYearInterestRate = new InterestRate(0.017M);
        var purchaseDate = new DateTime(2021, 03, 18);
        var calculationDate = new DateTime(2024, 01, 04);

        var edoBond = CreateEdoTreasuryBond(volume, firstYearInterestRate, margin, purchaseDate);
        var inflationRates = GetRealInflationRatesForEdo0331();

        // act
        var result = edoBond.GetAmount(inflationRates, DateOnly.FromDateTime(calculationDate), volume,
            new DateTime(2025, 01, 01));

        // assert
        const decimal expectedAmount1 = 4363.90M;
        const decimal expectedAmount2 = 4356.08M;
        var roundedResult = Math.Round(result, 0);

        roundedResult.ShouldBeInRange(expectedAmount2 * 0.98M, expectedAmount1 * 1.02M);
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

    private static ChronologicalInflationRates GetRealInflationRatesForEdo0931() => new(new List<InflationRate>
    {
        new(0.0590M, Currencies.PLN, 2021, 09),
        new(0.0680M, Currencies.PLN, 2021, 10),
        new(0.0780M, Currencies.PLN, 2021, 11),
        new(0.0860M, Currencies.PLN, 2021, 12),
        new(0.0940M, Currencies.PLN, 2022, 01),
        new(0.0850M, Currencies.PLN, 2022, 02),
        new(0.1100M, Currencies.PLN, 2022, 03),
        new(0.1240M, Currencies.PLN, 2022, 04),
        new(0.1390M, Currencies.PLN, 2022, 05),
        new(0.1550M, Currencies.PLN, 2022, 06),
        new(0.1560M, Currencies.PLN, 2022, 07),
        new(0.1610M, Currencies.PLN, 2022, 08),
                
        new(0.1720M, Currencies.PLN, 2022, 09),
        new(0.1790M, Currencies.PLN, 2022, 10),
        new(0.1750M, Currencies.PLN, 2022, 11),
        new(0.1660M, Currencies.PLN, 2022, 12),
        new(0.1660M, Currencies.PLN, 2023, 01),
        new(0.1840M, Currencies.PLN, 2023, 02),
        new(0.1610M, Currencies.PLN, 2023, 03),
        new(0.1470M, Currencies.PLN, 2023, 04),
        new(0.1300M, Currencies.PLN, 2023, 05),
        new(0.1150M, Currencies.PLN, 2023, 06),
        new(0.1080M, Currencies.PLN, 2023, 07),
        new(0.1010M, Currencies.PLN, 2023, 08),

        new(0.0820M, Currencies.PLN, 2023, 09),
        new(0.0660M, Currencies.PLN, 2023, 10),
    });
        
    private static ChronologicalInflationRates GetRealInflationRatesForEdo0331() => new(new List<InflationRate>
    {
        new(0.0320M, Currencies.PLN, 2021, 03),
        new(0.0430M, Currencies.PLN, 2021, 04),
        new(0.0470M, Currencies.PLN, 2021, 05),
        new(0.0440M, Currencies.PLN, 2021, 06),
        new(0.0500M, Currencies.PLN, 2021, 07),
        new(0.0550M, Currencies.PLN, 2021, 08),
        new(0.0590M, Currencies.PLN, 2021, 09),
        new(0.0680M, Currencies.PLN, 2021, 10),
        new(0.0780M, Currencies.PLN, 2021, 11),
        new(0.0860M, Currencies.PLN, 2021, 12),
        new(0.0940M, Currencies.PLN, 2022, 01),
        new(0.0850M, Currencies.PLN, 2022, 02),
                
        new(0.1100M, Currencies.PLN, 2022, 03),
        new(0.1240M, Currencies.PLN, 2022, 04),
        new(0.1390M, Currencies.PLN, 2022, 05),
        new(0.1550M, Currencies.PLN, 2022, 06),
        new(0.1560M, Currencies.PLN, 2022, 07),
        new(0.1610M, Currencies.PLN, 2022, 08),
        new(0.1720M, Currencies.PLN, 2022, 09),
        new(0.1790M, Currencies.PLN, 2022, 10),
        new(0.1750M, Currencies.PLN, 2022, 11),
        new(0.1660M, Currencies.PLN, 2022, 12),
        new(0.1660M, Currencies.PLN, 2023, 01),
        new(0.1840M, Currencies.PLN, 2023, 02),
                
        new(0.1610M, Currencies.PLN, 2023, 03),
        new(0.1470M, Currencies.PLN, 2023, 04),
        new(0.1300M, Currencies.PLN, 2023, 05),
        new(0.1150M, Currencies.PLN, 2023, 06),
        new(0.1080M, Currencies.PLN, 2023, 07),
        new(0.1010M, Currencies.PLN, 2023, 08),
        new(0.0820M, Currencies.PLN, 2023, 09),
        new(0.0660M, Currencies.PLN, 2023, 10),
        new(0.0660M, Currencies.PLN, 2023, 11),
    });
    #endregion
}