namespace InvestTracker.Shared.Abstractions.Exceptions;

public interface IExceptionToResponse
{
    ExceptionResponse Convert(Exception exception);
}