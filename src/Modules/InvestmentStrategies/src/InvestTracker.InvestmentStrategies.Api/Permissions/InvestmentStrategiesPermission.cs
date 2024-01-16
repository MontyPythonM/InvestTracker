namespace InvestTracker.InvestmentStrategies.Api.Permissions;

public enum InvestmentStrategiesPermission
{
    CreateInvestmentStrategy,
    AddPortfolio,
    ShareInvestmentStrategy,
    AddCashAsset,
    AddEdoAsset,
    GetPortfolioFinancialAssets,
    GetPortfolios,
    GetPortfolioDetails,
    GetInvestmentStrategies,
    GetInvestmentStrategyDetails,
    GetCashChart,
    AddCashTransaction,
    DeductCashTransaction,
    GetCash,
    RemoveCashTransaction,
    SeedInflationRates,
    ForceSeedInflationRates,
    GetEdoBond,
    GetEdoVolumeChart,
    GetEdoAmountChart,
    GetCoiBond,
    GetCoiVolumeChart,
    GetCoiAmountChart,
    AddCoiAsset
}