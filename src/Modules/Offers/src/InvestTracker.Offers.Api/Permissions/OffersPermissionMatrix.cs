using System.Reflection;
using InvestTracker.Shared.Abstractions.Authorization;

namespace InvestTracker.Offers.Api.Permissions;

internal sealed class OffersPermissionMatrix : IModulePermissionMatrix
{
    public string GetModuleName() => Assembly.GetExecutingAssembly().GetName().Name!;

    public List<Permission> Permissions { get; } = new()
    {
        new Permission(SystemSubscription.Advisor, "CreateOffer"),
        new Permission(SystemSubscription.Advisor, "UpdateOffer"),
        new Permission(SystemSubscription.Advisor, "DeleteOffer")
    };
}