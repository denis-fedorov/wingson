using WingsOn.Domain.Exceptions;
using WingsOn.WebApi.Exceptions;

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
        context.Response.ContentType = "application/json";

        // a small hack for incorrect date-time format mapping
        var message = exception is BadHttpRequestException
            ? "Invalid properties type in the model (e.g. DateTime format)"
            : exception.Message;

        return context.Response.WriteAsJsonAsync(value: new { errorMessage = message });

        static int GetStatusCode(Exception exception) => exception switch
        {
            // a special case - bad date-time format
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            
            InvalidGenderParamException => StatusCodes.Status400BadRequest,
            InvalidUpdateAddressModelException => StatusCodes.Status400BadRequest,
            InvalidAddPassengerModelException => StatusCodes.Status400BadRequest,
            PassengerAlreadyExistsOnFlightException => StatusCodes.Status400BadRequest,
            PersonNotFoundException => StatusCodes.Status404NotFound,
            FlightNotFoundException => StatusCodes.Status404NotFound,
            PassengerNotFoundForFlightException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}