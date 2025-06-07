using System.Text.Json;

namespace skills_test.Adapters.Controllers.Middlewares;

public class ExeptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExeptionMiddleware> _logger;

    public ExeptionMiddleware
    (
        RequestDelegate next,
        ILogger<ExeptionMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var error = new ErrorResponse(ex.Message);

            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
    }
}