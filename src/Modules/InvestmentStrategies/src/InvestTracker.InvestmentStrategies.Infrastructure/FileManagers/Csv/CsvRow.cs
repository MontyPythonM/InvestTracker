namespace InvestTracker.InvestmentStrategies.Infrastructure.FileManagers.Csv;

internal sealed class CsvRow
{
    public List<string> Values { get; set; }

    public CsvRow(IEnumerable<string> values)
    {
        Values = values.ToList();
    }
}