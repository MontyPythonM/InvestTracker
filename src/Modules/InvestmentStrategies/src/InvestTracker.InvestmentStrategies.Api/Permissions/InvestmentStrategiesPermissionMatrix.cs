﻿using System.Reflection;
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
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.AddCashAsset.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.AddEdoAsset.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.GetPortfolioFinancialAssets.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.GetPortfolios.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.GetPortfolioDetails.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.GetInvestmentStrategies.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.GetInvestmentStrategyDetails.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.GetCashChart.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.AddCashTransaction.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.DeductCashTransaction.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.GetCash.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.RemoveCashTransaction.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.GetEdoBond.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.GetEdoVolumeChart.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.GetEdoAmountChart.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.GetCoiBond.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.GetCoiVolumeChart.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.GetCoiAmountChart.ToString()),
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.AddCoiAsset.ToString()),

        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.CreateInvestmentStrategy.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.AddPortfolio.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.ShareInvestmentStrategy.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.AddCashAsset.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.AddEdoAsset.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.GetPortfolioFinancialAssets.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.GetPortfolios.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.GetPortfolioDetails.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.GetInvestmentStrategies.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.GetInvestmentStrategyDetails.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.GetCashChart.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.AddCashTransaction.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.DeductCashTransaction.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.RemoveCashTransaction.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.GetCash.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.GetEdoBond.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.GetEdoVolumeChart.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.GetEdoAmountChart.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.GetCoiBond.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.GetCoiVolumeChart.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.GetCoiAmountChart.ToString()),
        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.AddCoiAsset.ToString()),
        
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.CreateInvestmentStrategy.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.AddPortfolio.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.ShareInvestmentStrategy.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.AddCashAsset.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.AddEdoAsset.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.GetPortfolioFinancialAssets.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.GetPortfolios.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.GetPortfolioDetails.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.GetInvestmentStrategies.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.GetInvestmentStrategyDetails.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.GetCashChart.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.AddCashTransaction.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.DeductCashTransaction.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.GetCash.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.RemoveCashTransaction.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.GetEdoBond.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.GetEdoVolumeChart.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.GetEdoAmountChart.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.GetCoiBond.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.GetCoiVolumeChart.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.GetCoiAmountChart.ToString()),
        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.AddCoiAsset.ToString()),
        
        new(SystemRole.BusinessAdministrator, InvestmentStrategiesPermission.SeedInflationRates.ToString()),
        
        new(SystemRole.SystemAdministrator, InvestmentStrategiesPermission.SeedInflationRates.ToString()),
        new(SystemRole.SystemAdministrator, InvestmentStrategiesPermission.ForceSeedInflationRates.ToString()),
    };
}