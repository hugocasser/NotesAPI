namespace Notes.WebApi.Middleware;

public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(
        this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<CustomExceptionsHandleMiddleware>();
    }
}