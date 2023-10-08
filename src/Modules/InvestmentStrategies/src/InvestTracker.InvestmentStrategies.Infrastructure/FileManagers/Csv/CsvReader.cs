namespace InvestTracker.InvestmentStrategies.Infrastructure.FileManagers.Csv;

internal sealed class CsvReader : ICsvReader
{
    private readonly string[] _lineEnd = { "\r\n", "\n" };
    private readonly string[] _columnDelimiter = { ";" };
    private const int IndexOfNotFoundResult = -1;
    
    public string Read(string filePath) => File.ReadAllText(filePath);

    public CsvTable Parse(string data)
    {
        var lines = data.Split(_lineEnd, StringSplitOptions.RemoveEmptyEntries);
        var rows = lines.Select(line => new CsvRow(line.Split(_columnDelimiter, StringSplitOptions.None)));

        return new CsvTable(rows.ToList());
    }

    public IEnumerable<string> GetColumnValues(CsvTable table, string columnName, int headerRowIndex = 0)
    {
        var columnIndex = table.GetRow(headerRowIndex).Values.IndexOf(columnName);

        if (columnIndex.Equals(IndexOfNotFoundResult))
        {
            throw new KeyNotFoundException($"Column name: {columnName} was not found in table row number: {headerRowIndex}");
        }

        return table.Rows
            .Select(row => row.Values[columnIndex])
            .ToList();
    }
}