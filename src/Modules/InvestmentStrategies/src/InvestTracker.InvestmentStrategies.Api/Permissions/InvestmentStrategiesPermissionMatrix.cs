using System.Reflection;
using InvestTracker.Shared.Abstractions.Authorization;

namespace InvestTracker.InvestmentStrategies.Api.Permissions;

public class InvestmentStrategiesPermissionMatrix : IModulePermissionMatrix
{
    public string GetModuleName() => Assembly.GetExecutingAssembly().GetName().Name!;

    public ISet<Permission> Permissions { get; } = new HashSet<Permission>
    {
        new(SystemRole.BusinessAdministrator, InvestmentStrategiesPermission.SaveSeedExchangeRate.ToString()),
        new(SystemRole.SystemAdministrator, InvestmentStrategiesPermission.SaveSeedExchangeRate.ToString()),
        new(SystemRole.SystemAdministrator, InvestmentStrategiesPermission.ForceSeedExchangeRate.ToString()),
    };
}