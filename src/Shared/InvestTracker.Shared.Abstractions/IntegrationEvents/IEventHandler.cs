namespace InvestTracker.Shared.Abstractions.IntegrationEvents;

public interface IEventHandler<in TEvent> where TEvent : class, IEvent
{
    Task HandleAsync(TEvent @event);
}