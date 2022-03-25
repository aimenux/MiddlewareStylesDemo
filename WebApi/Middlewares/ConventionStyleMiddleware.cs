namespace WebApi.Middlewares
{
    public class ConventionStyleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ConventionStyleMiddleware> _logger;

        public ConventionStyleMiddleware(RequestDelegate next, ILogger<ConventionStyleMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Starting convention middleware");
            context.Response.Headers.Add("C-CustomHeader", "Convention Middleware");
            await _next(context);
            _logger.LogInformation("Stopping convention middleware");
        }
    }

    public static class ConventionStyleMiddlewareExtensions
    {
        public static void UseConventionStyleMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ConventionStyleMiddleware>();
        }
    }
}
