using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PdmLite.Exceptions;

public class DocumentAlreadyExistsExceptionHandler(ILogger<DocumentAlreadyExistsExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken ct)
    {
        if (exception is not DocumentAlreadyExistsException alreadyExistsEx)
            return false;
        
        logger.LogWarning("Document [{Path}] already exists: {Message}", httpContext.Request.Path, alreadyExistsEx.Message);
        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status409Conflict,
            Title = "Document already exists",
            Detail = alreadyExistsEx.Message,
            Instance = httpContext.Request.Path
        };
        
        await httpContext.Response.WriteAsJsonAsync(problemDetails, ct);
        return true;
    }
}