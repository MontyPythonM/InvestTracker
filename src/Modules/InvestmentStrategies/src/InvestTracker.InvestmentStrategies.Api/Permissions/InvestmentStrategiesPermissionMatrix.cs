using System.Reflection;
using InvestTracker.Shared.Abstractions.Authorization;

namespace InvestTracker.InvestmentStrategies.Api.Permissions;

public class InvestmentStrategiesPermissionMatrix : IModulePermissionMatrix
{
    public string GetModuleName() => Assembly.GetExecutingAssembly().GetName().Name!;

    public ISet<Permission> Permissions { get; } = new HashSet<Permission>
    {
        new(SystemSubscription.Advisor, InvestmentStrategiesPermission.CreateInvestmentStrategy.ToString()),

        new(SystemSubscription.StandardInvestor, InvestmentStrategiesPermission.CreateInvestmentStrategy.ToString()),

        new(SystemSubscription.ProfessionalInvestor, InvestmentStrategiesPermission.CreateInvestmentStrategy.ToString()),
        
        new(SystemRole.BusinessAdministrator, InvestmentStrategiesPermission.SafeSeedExchangeRate.ToString()),
        
        new(SystemRole.SystemAdministrator, InvestmentStrategiesPermission.SafeSeedExchangeRate.ToString()),
        new(SystemRole.SystemAdministrator, InvestmentStrategiesPermission.ForceSeedExchangeRate.ToString()),
    };
}