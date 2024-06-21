namespace InvestTracker.Users.Core.Enums;

public enum SubscriptionChangeSource
{
    NeverChanged = 0,
    FromPayment,
    FromAdministrator,
    Expired
}