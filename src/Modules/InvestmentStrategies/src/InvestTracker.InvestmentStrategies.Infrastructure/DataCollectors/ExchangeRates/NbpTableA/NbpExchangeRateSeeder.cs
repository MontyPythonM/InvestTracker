using System.Text.RegularExpressions;
using InvestTracker.InvestmentStrategies.Domain.Asset.Consts;
using InvestTracker.InvestmentStrategies.Infrastructure.Exceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.FileManagers.Csv;
using InvestTracker.InvestmentStrategies.Infrastructure.Options;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.ExchangeRates.NbpTableA;

internal sealed class NbpExchangeRateSeeder : IExchangeRateSeeder
{
    private const string CurrencyCodeRegex = @"([A-Z]{3})";
    private const string CurrencyMultiplierRegex = @"(\d+)";
    private const string DateRegex = @"^\d{8}$";
    private const int DefaultCurrencyMultiplier = 1;
    private const string RowIdentifierColumn = "numer tabeli";
    
    private readonly ICsvReader _csvReader;
    private readonly ITimeProvider _timeProvider;
    private readonly InvestmentStrategiesDbContext _investmentStrategiesDbContext;
    private readonly ExchangeRateOptions _exchangeRateOptions;

    public NbpExchangeRateSeeder(ICsvReader csvReader, ITimeProvider timeProvider, 
        InvestmentStrategiesDbContext investmentStrategiesDbContext, ExchangeRateOptions exchangeRateOptions)
    {
        _csvReader = csvReader;
        _timeProvider = timeProvider;
        _investmentStrategiesDbContext = investmentStrategiesDbContext;
        _exchangeRateOptions = exchangeRateOptions;
    }

    public async Task SeedAsync(bool forceSeed = false, CancellationToken token = default)
    {
        if (!await _investmentStrategiesDbContext.Database.CanConnectAsync(token))
        {
            throw new CannotConnectToDatabaseException();
        }

        if (!forceSeed && await IsAnyExchangeRateExists(token))
        {
            throw new ExchangeRateAlreadySeededException();
        }

        var fullPath = GetNbpCsvPath();
        var filesPath = Directory
            .GetFiles(fullPath)
            .Where(path => path.EndsWith(".csv"));
        
        var exchangeRates = new List<ExchangeRate>();
        foreach (var path in filesPath)
        {
            var rates = GetExchangeRates(path, token);
            exchangeRates.AddRange(rates);
        }

        await _investmentStrategiesDbContext.ExchangeRates.AddRangeAsync(exchangeRates, token);
        await _investmentStrategiesDbContext.SaveChangesAsync(token);
    }

    private IEnumerable<ExchangeRate> GetExchangeRates(string filePath, CancellationToken token = default)
    {
        var file = _csvReader.Read(filePath);
        var csv = _csvReader.Parse(file);
        
        var headers = csv.GetRow(0);
        var rowIdentifierColumnIndex = headers.Values.FindIndex(value => value.Contains(RowIdentifierColumn));
        var rowsWithoutHeaders = csv.Rows
            .Where(row => IsDateFormat(row.Values[0]))
            .ToList();

        var exchangeRates = new List<ExchangeRate>();
        foreach (var row in rowsWithoutHeaders)
        {
            var date = DateOnly.ParseExact(row.Values[0], "yyyyMMdd", null);
            
            for (var rowIndex = 0; rowIndex < row.Values.Count; rowIndex++)
            {
                token.ThrowIfCancellationRequested();
                if (!IsCurrencySupported(headers.Values[rowIndex]) || !decimal.TryParse(row.Values[rowIndex], out var amount))
                {
                    continue;
                }

                var multiplier = GetCurrencyMultiplier(row.Values[rowIndex]);
                var exchangeRate = new ExchangeRate(
                    Guid.NewGuid(),
                    GetCurrencyCode(headers.Values[rowIndex]),
                    "PLN",
                    date,
                    _timeProvider.Current(),
                    amount / multiplier,
                    row.Values[rowIdentifierColumnIndex]
                );

                exchangeRates.Add(exchangeRate);
            }
        }
        
        return exchangeRates;
    }

    private async Task<bool> IsAnyExchangeRateExists(CancellationToken token) 
        => await _investmentStrategiesDbContext.ExchangeRates.AnyAsync(token);

    private static bool IsCurrencySupported(string header)
        => AssetConstants.AvailableCurrencies.Contains(GetCurrencyCode(header));
    
    private static string GetCurrencyCode(string input)
    {
        var match = Regex.Match(input, CurrencyCodeRegex);
        return match.Success ? match.Groups[1].Value : string.Empty;
    }

    private static int GetCurrencyMultiplier(string input)
    {
        var match = Regex.Match(input, CurrencyMultiplierRegex);
        if (match.Success)
        {
            var multiplier = int.Parse(match.Groups[1].Value);
            return multiplier > 1 ? multiplier : DefaultCurrencyMultiplier;
        }

        return DefaultCurrencyMultiplier;
    }

    private static bool IsDateFormat(string input)
        => Regex.Match(input, DateRegex).Success;

    private string GetNbpCsvPath()
    {
        var basePath = GoUpDirectory(Environment.CurrentDirectory, 2);
        return Path.Combine(basePath, _exchangeRateOptions.NbpCsvPath);
    }

    private string GoUpDirectory(string path, int goUpNumber)
    {
        for (var i = 0; i < goUpNumber; i++)
        {
            path = Directory.GetParent(path)?.FullName!;
        }

        return path;
    }
}