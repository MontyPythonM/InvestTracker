namespace InvestTracker.InvestmentStrategies.Infrastructure.FileManagers.Csv;

internal interface ICsvReader
{
    string Read(string filePath);
    CsvTable Parse(string data);
    IEnumerable<string> GetColumnValues(CsvTable table, string columnName, int headerRowIndex = 0);
}