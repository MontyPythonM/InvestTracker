namespace InvestTracker.Shared.Abstractions.Commands;

public interface ICommandDispatcher
{
    Task SendAsync<TCommand>(TCommand command, CancellationToken token = default) where TCommand : class, ICommand;
}