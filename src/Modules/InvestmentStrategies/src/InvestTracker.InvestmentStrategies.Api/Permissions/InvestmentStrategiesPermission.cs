namespace InvestTracker.InvestmentStrategies.Api.Permissions;

public enum InvestmentStrategiesPermission
{
    CreateInvestmentStrategy,
    AddPortfolio,
    ShareInvestmentStrategy,
    CreateCashAsset,
    CreateEdoAsset,
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
    GetEdoTreasuryBond
}