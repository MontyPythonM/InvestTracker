namespace InvestTracker.InvestmentStrategies.Domain.Asset.Consts;

public static class Constants
{
    public static readonly HashSet<string> AvailableCurrencies = new()
    {
        "PLN", "USD", "EUR", "GBP", "JPY", "CHF", "NOK", "SEK", "CNY", "KRW", "RUB", "BRL", "PHP", "LVL", "LTL", "BGN", "DKK", "CZK", "HUF"
    };
}