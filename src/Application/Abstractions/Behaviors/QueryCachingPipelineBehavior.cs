using Application.Abstractions.Caching;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.Abstractions.Behaviors;

internal sealed partial class QueryCachingPipelineBehavior<TRequest, TResponse>(
    ICacheService cacheService,
    ILogger<QueryCachingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse? cachedResult = await cacheService.GetAsync<TResponse>(
            request.CacheKey,
            cancellationToken);

        string requestName = typeof(TRequest).Name;
        if (cachedResult is not null)
        {
            LogCacheHit(logger, requestName);

            return cachedResult;
        }

        LogCacheMiss(logger, requestName);

        TResponse result = await next();

        if (result.IsSuccess)
        {
            await cacheService.SetAsync(
                request.CacheKey,
                result,
                request.Expiration,
                cancellationToken);
        }

        return result;
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Cache hit for {RequestName}")]
    private static partial void LogCacheHit(ILogger logger, string requestName);

    [LoggerMessage(Level = LogLevel.Information, Message = "Cache miss for {RequestName}")]
    private static partial void LogCacheMiss(ILogger logger, string requestName);
}
