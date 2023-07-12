namespace InvestTracker.Shared.Abstractions.Authorization;

public static class SystemSubscription
{
    public const string StandardInvestor = "StandardInvestor";
    public const string ProfessionalInvestor = "ProfessionalInvestor";
    public const string Advisor = "Advisor";
    
    public static readonly IReadOnlySet<string> Subscriptions = new HashSet<string>()
    {
        StandardInvestor, ProfessionalInvestor, Advisor
    };
}