using IdentityRegistration.Application.Configuration.Exceptions;
using IdentityRegistration.Shared.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IdentityRegistration.Application.Configuration.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApiException error)
        {
            await HandleExceptionAsync(context, error.Message ?? nameof(ApiException));
        }
        catch (System.ComponentModel.DataAnnotations.ValidationException error)
        {
            await HandleExceptionAsync(context, error.Message);
        }
        catch (ValidationException error)
        {
            var validationErrors = error.ValidationErrors.Select(e => new { e.PropertyName, e.ErrorMessage });
            await HandleExceptionAsync(context, JsonConvert.SerializeObject(validationErrors));
        }
        catch (Exception error)
        {
            await HandleExceptionAsync(context, error.Message);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, string? message)
    {
        HttpResponse response = context.Response;
        response.ContentType = "application/json";
        Response<int?> responseModel = new Response<int?>(message);

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        string result = JsonConvert.SerializeObject(responseModel, settings);

        _logger.LogError($"Message: {message}, Response: {result}");

        await response.WriteAsync(result);
    }
}
