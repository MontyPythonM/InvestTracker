using System.Net;
using InvestTracker.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace InvestTracker.Shared.Infrastructure.Exceptions;

internal class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly IExceptionToResponse _exceptionToResponse;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, IExceptionToResponse exceptionToResponse)
    {
        _logger = logger;
        _exceptionToResponse = exceptionToResponse;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var exceptionResponse = _exceptionToResponse.Convert(exception);
        context.Response.StatusCode = (int)(exceptionResponse?.StatusCode ?? HttpStatusCode.InternalServerError);

        var response = exceptionResponse?.Response;

        if (response is null)
        {
            return;
        }
        
        await context.Response.WriteAsJsonAsync(response);
    }
}