using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HallOfFame.BusinessLogic.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest
    : IRequest<TResponse>
{
    private readonly ILogger _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }


    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        string requestName = typeof(TRequest).Name;
        string jsonRequest = JsonSerializer.Serialize(request);
        _logger.LogInformation($"Request: {requestName} | {jsonRequest}");
        return await next();
    }
}