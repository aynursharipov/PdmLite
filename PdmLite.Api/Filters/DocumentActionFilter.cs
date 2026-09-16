using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PdmLite.Filters;

public class DocumentActionFilter(ILogger<DocumentActionFilter> logger) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();
        logger.LogInformation("{ActionDescriptorDisplayName} is started", context.ActionDescriptor.DisplayName);
        
        var executedAction = await next();
        stopwatch.Stop();
        
        if (executedAction.Exception != null && !executedAction.ExceptionHandled)
        {
            logger.LogError(executedAction.Exception, executedAction.Exception.Message);
        }
        
        logger.LogInformation("{ActionDescriptorDisplayName} is finished in {ElapsedMilliseconds}",
            context.ActionDescriptor.DisplayName, stopwatch.ElapsedMilliseconds);
    }
}