namespace InvestTracker.Shared.Infrastructure.Types;

public static class GuidExtensions
{
    public static Guid ToGuid(this string inputValue)
    {
        return Guid.TryParse(inputValue, out var parsedResult) ? parsedResult : throw new InvalidCastException();
    }
    
    public static Guid? ToNullableGuid(this string inputValue)
    {
        return Guid.TryParse(inputValue, out var parsedValue) ? parsedValue : null;
    }
}