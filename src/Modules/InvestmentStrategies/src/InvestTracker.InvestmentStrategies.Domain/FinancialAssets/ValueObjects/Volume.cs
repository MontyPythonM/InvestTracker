using InvestTracker.InvestmentStrategies.Domain.FinancialAssets.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.FinancialAssets.ValueObjects;

public record Volume
{
    public int Value { get; }
    
    public Volume(int value)
    {
        if (value is <= 0 or > 1_000_000_000)
        {
            throw new InvalidVolumeException(value);
        }

        Value = value;
    }
    
    public static implicit operator Volume(int value) => new(value);
    public static implicit operator int(Volume value) => value.Value;
};