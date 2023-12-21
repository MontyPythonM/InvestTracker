namespace InvestTracker.InvestmentStrategies.Infrastructure.FileManagers.Csv;

internal interface ICsvReader
{
    string Read(string filePath);
    public CsvTable Parse(string data, char delimiter = ';');
    IEnumerable<string> GetColumnValues(CsvTable table, string columnName, int headerRowIndex = 0);
}