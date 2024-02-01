namespace InvestTracker.Notifications.Core.Interfaces;

public interface IReceiverSynchronizer
{
    Task Synchronize(CancellationToken token);
}