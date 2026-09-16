using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PdmLite.Exceptions;

namespace PdmLite.Filters;

public class ApiExceptionFilter: IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not DocumentNotFoundException exception) 
            return;
        
        var problemDetails = new ProblemDetails()
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Document not found",
            Detail = exception.Message,
        };

        context.Result = new NotFoundObjectResult(problemDetails);
        context.ExceptionHandled = true;
    }
}