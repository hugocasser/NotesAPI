using System.Net;
using System.Text.Json;
using FluentValidation;
using Notes.Application.Common.Exceptions;

public class CustomExceptionsHandleMiddleware
{
    public readonly RequestDelegate _next;

    public CustomExceptionsHandleMiddleware(RequestDelegate next) =>
        _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsunc(context, ex);
        }
    }

    public Task HandleExceptionAsunc(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (ex)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            case NotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { errpr = ex.Message });
        }

        return context.Response.WriteAsync(result);
    }
}