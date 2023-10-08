namespace InvestTracker.InvestmentStrategies.Infrastructure.FileManagers.Csv;

internal sealed class CsvTable
{
    public List<CsvRow> Rows { get; set; }

    public CsvTable(List<CsvRow> rows)
    {
        Rows = rows;
    }

    public CsvRow GetRow(int rowNumber)
    {
        if (rowNumber < 0 || rowNumber > Rows.Count)
        {
            throw new ArgumentOutOfRangeException($"Row {rowNumber} is out of range");
        }
        
        return Rows[rowNumber];
    }
}