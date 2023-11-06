namespace InvestTracker.Shared.Abstractions.Time;

public interface ITimeProvider
{
    public DateTime Current();
    public DateOnly CurrentDate();
}