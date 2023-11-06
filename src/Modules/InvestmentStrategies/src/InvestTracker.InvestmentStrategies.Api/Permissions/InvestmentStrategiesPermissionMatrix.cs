using System.Reflection;
using InvestTracker.Shared.Abstractions.Authorization;

namespace InvestTracker.InvestmentStrategies.Api.Permissions;

public class InvestmentStrategiesPermissionMatrix : IModulePermissionMatrix
{
    public string GetModuleName() => Assembly.GetExecutingAssembly().GetName().Name!;

    public ISet<Permission> Permissions { get; } = new HashSet<Permission>
    {
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.CreateInvestmentStrategy.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.AddPortfolio.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.ShareInvestmentStrategy.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.CreateCashAsset.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.CreateEdoAsset.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.GetPortfolioFinancialAssets.ToString()),

        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.CreateInvestmentStrategy.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.AddPortfolio.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.ShareInvestmentStrategy.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.CreateCashAsset.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.CreateEdoAsset.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.GetPortfolioFinancialAssets.ToString()),

        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.CreateInvestmentStrategy.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.AddPortfolio.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.ShareInvestmentStrategy.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.CreateCashAsset.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.CreateEdoAsset.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.GetPortfolioFinancialAssets.ToString()),

        new(SystemRole.BusinessAdministrator, InvestmentStrategiesPermission.SafeSeedExchangeRate.ToString()),
        
        new(SystemRole.SystemAdministrator, InvestmentStrategiesPermission.SafeSeedExchangeRate.ToString()),
        new(SystemRole.SystemAdministrator, InvestmentStrategiesPermission.ForceSeedExchangeRate.ToString()),
    };
}