namespace WebApi.Middlewares;

public class FactoryStyleMiddleware : IMiddleware
{
    private readonly ILogger<FactoryStyleMiddleware> _logger;

    public FactoryStyleMiddleware(ILogger<FactoryStyleMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _logger.LogInformation("Starting factory middleware");
        context.Response.Headers.TryAdd("F-CustomHeader", "Factory Middleware");
        await next(context);
        _logger.LogInformation("Stopping factory middleware");
    }
}

public static class FactoryStyleMiddlewareExtensions
{
    public static void UseFactoryStyleMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<FactoryStyleMiddleware>();
    }
}