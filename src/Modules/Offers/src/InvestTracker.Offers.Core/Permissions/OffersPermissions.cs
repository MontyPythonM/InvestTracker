using InvestTracker.Shared.Abstractions.Authorization;

namespace InvestTracker.Offers.Core.Permissions;

internal class OffersPermissions : IModulePermissions
{
    public List<Permission> Permissions { get; set;  } = new()
    {
        new Permission(SystemSubscription.Advisor, "CreateOffer"),
        new Permission(SystemSubscription.Advisor, "UpdateOffer"),
        new Permission(SystemSubscription.Advisor, "DeleteOffer")
    };
    // public List<Permission> Permissions { get; } = new List<Permission>();
}