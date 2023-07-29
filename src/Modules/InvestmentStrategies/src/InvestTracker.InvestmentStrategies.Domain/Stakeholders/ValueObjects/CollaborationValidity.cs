using InvestTracker.InvestmentStrategies.Domain.InvestmentStrategies.Exceptions;

namespace InvestTracker.InvestmentStrategies.Domain.Stakeholders.ValueObjects;

public record CollaborationValidity
{
    public DateTime From { get; }
    public DateTime To { get; }

    public CollaborationValidity(DateTime from, DateTime to)
    {
        if (from > to)
        {
            throw new InvalidCollaborationValidityException(from, to);
        }

        From = from;
        To = to;
    }
}