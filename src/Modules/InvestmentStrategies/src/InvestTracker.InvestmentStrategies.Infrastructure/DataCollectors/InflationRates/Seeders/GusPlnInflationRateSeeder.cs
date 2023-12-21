using InvestTracker.InvestmentStrategies.Domain.Portfolios.Consts;
using InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Entities;
using InvestTracker.InvestmentStrategies.Infrastructure.Exceptions;
using InvestTracker.InvestmentStrategies.Infrastructure.FileManagers.Csv;
using InvestTracker.InvestmentStrategies.Infrastructure.Options;
using InvestTracker.InvestmentStrategies.Infrastructure.Persistence;
using InvestTracker.Shared.Abstractions.DDD.ValueObjects;
using InvestTracker.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvestTracker.InvestmentStrategies.Infrastructure.DataCollectors.InflationRates.Seeders;

/// <summary>
/// Download the latest version from: https://stat.gov.pl/obszary-tematyczne/ceny-handel/wskazniki-cen/wskazniki-cen-towarow-i-uslug-konsumpcyjnych-pot-inflacja-/miesieczne-wskazniki-cen-towarow-i-uslug-konsumpcyjnych-od-1982-roku/
/// </summary>
internal sealed class GusPlnInflationRateSeeder : IInflationRateSeeder
{
    private readonly InvestmentStrategiesDbContext _dbContext;
    private readonly ICsvReader _csvReader;
    private readonly InflationRateSeederOptions _seederOptions;
    private readonly ILogger<GusPlnInflationRateSeeder> _logger;
    private readonly ITimeProvider _timeProvider;
    
    public GusPlnInflationRateSeeder(InvestmentStrategiesDbContext dbContext, ICsvReader csvReader, 
    InflationRateSeederOptions seederOptions, ILogger<GusPlnInflationRateSeeder> logger, ITimeProvider timeProvider)
    {
        _dbContext = dbContext;
        _csvReader = csvReader;
        _seederOptions = seederOptions;
        _logger = logger;
        _timeProvider = timeProvider;
    }
    
    public async Task<string> SeedAsync(bool overrideCurrentValues = false, CancellationToken token = default)
    {
        if (!_seederOptions.Enabled)
        {
            throw new InflationRateSeederException("Seeder is disabled.");
        }
        
        if (!await _dbContext.Database.CanConnectAsync(token))
        {
            throw new CannotConnectToDatabaseException();
        }

        var path = _seederOptions.Path;
        if (!File.Exists(path))
        {
            throw new InflationRateSeederException($"Cannot find csv file in '{path}' directory.");
        }

        var file = _csvReader.Read(path);
        var csv = _csvReader.Parse(file);
        var inflationRates = new List<InflationRateEntity>();
        
        foreach (var row in csv.Rows.Skip(1))
        {
            token.ThrowIfCancellationRequested();
            
            if (row.Values[_seederOptions.RowTypeIdentifierColumnIndex] != _seederOptions.RowTypeIdentifier)
            {
                continue;
            }

            var parsedRow = ParseRowValues(row);

            if (IsNoValueInRow(parsedRow.percentageValue))
            {
                continue;
            }

            var metadata = $"source: 'csv seeder', selector: '{_seederOptions.SelectorName}'";
            
            var inflationRate = new InflationRateEntity(Guid.NewGuid(), Currencies.PLN, parsedRow.monthlyDate, 
                _timeProvider.Current(), parsedRow.percentageValue, metadata);
            
            inflationRates.Add(inflationRate);
        }

        if (overrideCurrentValues)
        {
            await _dbContext.InflationRates.ExecuteDeleteAsync(token);
        }
        else
        {
            inflationRates = await GetNewInflationRatesAsync(inflationRates, token);
        }

        await _dbContext.InflationRates.AddRangeAsync(inflationRates, token);
        await _dbContext.SaveChangesAsync(token);

        var message = $"Inflation rates seeded. Currency: '{Currencies.PLN}', Processed rows: '{inflationRates.Count}', FinishedAt: '{_timeProvider.Current()}'.";
        _logger.LogInformation(message);
        return message;
    }

    private (MonthlyDate monthlyDate, decimal percentageValue) ParseRowValues(CsvRow row)
    {
        var yearString = row.Values[_seederOptions.YearColumnIndex];
        var monthString = row.Values[_seederOptions.MonthColumnIndex];
        var valueString = row.Values[_seederOptions.ValueColumnIndex];

        var canParseYear = int.TryParse(yearString, out var year);
        var canParseMonth = int.TryParse(monthString, out var month);
        var canParseValue = decimal.TryParse(valueString, out var value);

        if (!canParseYear && !canParseMonth && !canParseValue)
        {
            var message = $"Cannot parse row with values: year '{yearString}', month '{monthString}', value: '{valueString}'.";
            
            if (!_seederOptions.IgnoreErrors)
            {
                throw new InflationRateSeederException(message);
            }

            _logger.LogError(message);
        }

        return (new MonthlyDate(year, month), (value - 100) / 100);
    }

    private async Task<List<InflationRateEntity>> GetNewInflationRatesAsync(IEnumerable<InflationRateEntity> inflationRates, 
        CancellationToken token)
    {
        var existingInflationRateDates = await _dbContext.InflationRates
            .Select(rate => rate.MonthlyDate)
            .ToListAsync(token);
            
        return inflationRates
            .Where(rate => !existingInflationRateDates.Contains(rate.MonthlyDate))
            .ToList();
    }
    
    private static bool IsNoValueInRow(decimal percentageValue) => percentageValue.Equals(-1);
}