using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using SharedKernel;

namespace Application.Abstractions.Behaviors;

internal sealed partial class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        LogProcessing(logger, requestName);

        TResponse result = await next();

        if (result.IsSuccess)
        {
            LogCompleted(logger, requestName);
        }
        else
        {
            using (LogContext.PushProperty("Error", result.Error, true))
            {
                LogCompletedWithError(logger, requestName);
            }
        }

        return result;
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Processing request {RequestName}")]
    private static partial void LogProcessing(ILogger logger, string requestName);

    [LoggerMessage(Level = LogLevel.Information, Message = "Completed request {RequestName}")]
    private static partial void LogCompleted(ILogger logger, string requestName);

    [LoggerMessage(Level = LogLevel.Error, Message = "Completed request {RequestName} with error")]
    private static partial void LogCompletedWithError(ILogger logger, string requestName);
}
