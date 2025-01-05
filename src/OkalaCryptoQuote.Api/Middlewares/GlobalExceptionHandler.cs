using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OkalaCryptoQuote.Domain.Base;

namespace OkalaCryptoQuote.Api.Middlewares;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = exception switch
        {
            BadHttpRequestException e => HandleBadRequest(e),
            _ => HandleInternalServerError()
        };

        httpContext.Response.StatusCode = problemDetails.Status!.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private ProblemDetails HandleBadRequest(BadHttpRequestException exception)
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Detail = exception.Message
        };
    }

    private ProblemDetails HandleInternalServerError()
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
        };
    }
}
