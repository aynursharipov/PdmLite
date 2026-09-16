using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PdmLite.Exceptions;

public class DocumentNotFoundExceptionHandler(ILogger<DocumentNotFoundExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not DocumentNotFoundException notFoundEx)
            return false;
            
        logger.LogWarning("Document [{Path}] not found: {Message}", httpContext.Request.Path, notFoundEx.Message);
        var problemDeatils = new ProblemDetails()
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Document not found",
            Detail = notFoundEx.Message,
            Instance = httpContext.Request.Path
        };
            
        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        await httpContext.Response.WriteAsJsonAsync(problemDeatils, cancellationToken);
            
        return true;
    }
}