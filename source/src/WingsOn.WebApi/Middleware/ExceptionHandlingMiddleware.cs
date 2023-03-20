using WingsOn.Domain.Exceptions;

namespace WingsOn.WebApi.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleException(context, exception);
        }
    }
    
    private static Task HandleException(HttpContext context, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "text/plain";

        return context.Response.WriteAsync(exception.Message);

        static int GetStatusCode(Exception exception) => exception switch
        {
            PersonNotFoundException => StatusCodes.Status404NotFound,
            FlightNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}