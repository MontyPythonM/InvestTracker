﻿namespace InvestTracker.InvestmentStrategies.Domain.Portfolios.Consts;

public static class Currencies
{
    public static readonly List<string> AvailableCurrencies = new()
    {
        "PLN", "USD", "PHP", "SEK","KRW", "DKK", "EUR", "CNY", "BRL", "CZK", "BGN", "JPY", "HUF", "NOK", "CHF", "GBP"
    };
    
    public const string PLN = "PLN";
    public const string USD = "USD";
    public const string EUR = "EUR";
    public const string GBP = "GBP";
    public const string JPY = "JPY";
    public const string CHF = "CHF";
    public const string NOK = "NOK";
    public const string SEK = "SEK";
    public const string CNY = "CNY";
    public const string KRW = "KRW";
    public const string BRL = "BRL";
    public const string PHP = "PHP";
    public const string BGN = "BGN";
    public const string DKK = "DKK";
    public const string CZK = "CZK";
    public const string HUF = "HUF";

    public static bool IsSupported(string currency) => AvailableCurrencies.Contains(currency.ToUpper());
}