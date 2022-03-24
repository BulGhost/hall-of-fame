using System.Net;
using System.Text.Json;
using FluentValidation;
using HallOfFame.BusinessLogic.Resources;
using HallOfFame.Domain.Exceptions.Base;
using HallOfFame.WebApi.ViewModels;

namespace HallOfFame.WebApi.Middlewares;

public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private const string _defaultErrorMessage = "An error occurred on the server side, please contact support";
    private const string _requestCancelledMessage = "Request was cancelled";
    private readonly ILogger _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = new ErrorDetails { ErrorMessage = exception.Message };

        switch (exception)
        {
            case ValidationException validationException
                when validationException.Errors.First().ErrorCode == TextResources.NotFoundErrorCode:
                code = HttpStatusCode.NotFound;
                result.ErrorMessage = validationException.Errors.FirstOrDefault()?.ErrorMessage;
                _logger.LogInformation("Not found(404). Error message: {0}", result.ErrorMessage);
                break;
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result.ErrorMessage = validationException.Errors.FirstOrDefault()?.ErrorMessage;
                _logger.LogInformation("Validation failed. Error message: {0}", result.ErrorMessage);
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                _logger.LogInformation(exception.Message);
                break;
            case BadRequestException:
                code = HttpStatusCode.BadRequest;
                _logger.LogInformation("Bad request(400). Error message: {0}", exception.Message);
                break;
            case OperationCanceledException:
                code = HttpStatusCode.BadRequest;
                result.ErrorMessage = _requestCancelledMessage;
                _logger.LogInformation("Request was cancelled");
                break;
            default:
                _logger.LogError(exception, "Unexpected error occurred");
                result.ErrorMessage = _defaultErrorMessage;
                break;
        }

        result.StatusCode = (int)code;
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = result.StatusCode;

        string response = JsonSerializer.Serialize(result, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        await context.Response.WriteAsync(response);
    }
}