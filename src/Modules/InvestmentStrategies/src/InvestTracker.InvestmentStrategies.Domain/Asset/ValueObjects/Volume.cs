using InvestTracker.InvestmentStrategies.Domain.Asset.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Asset.ValueObjects;

public record Volume
{
    public int Value { get; }

    public Volume(int value)
    {
        if (value < 0)
        {
            throw new InvalidVolumeException(value);
        }

        Value = value;
    }
    
    public static implicit operator Volume(int value) => new(value);
    public static implicit operator int(Volume value) => value.Value;
}