using System.Collections.Concurrent;
using System.Net;
using Humanizer;
using InvestTracker.Shared.Abstractions.Exceptions;

namespace InvestTracker.Shared.Infrastructure.Exceptions;

internal class ExceptionToResponse : IExceptionToResponse
{
    private static readonly ConcurrentDictionary<Type, string> ExceptionCodes = new();

    public ExceptionResponse Convert(Exception exception) 
        => exception switch
        {
            InvestTrackerException ex => new ExceptionResponse(new ErrorsResponse(new Error(GetExceptionCode(ex), ex.Message)), 
                HttpStatusCode.BadRequest),
            
            _ => new ExceptionResponse(new Error("error", "There was an error."), 
                HttpStatusCode.InternalServerError)
        };

    private record Error(string ExceptionCode, string ExceptionMessage);
    private record ErrorsResponse(params Error[] Errors);

    /// <summary>
    /// Returns custom exception name in snake case convention
    /// </summary>
    private static string GetExceptionCode(object exception)
    {
        var type = exception.GetType();
        return ExceptionCodes.GetOrAdd(type, type.Name.Underscore().Replace("_exception", string.Empty));
    }
}