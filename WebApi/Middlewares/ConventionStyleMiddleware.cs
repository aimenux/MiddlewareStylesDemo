namespace WebApi.Middlewares
{
    public class ConventionStyleMiddleware
    {
        private readonly RequestDelegate _next;

        public ConventionStyleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<ConventionStyleMiddleware> logger)
        {
            logger.LogInformation("Starting convention middleware");
            context.Response.Headers.Add("C-CustomHeader", "Convention Middleware");
            await _next(context);
            logger.LogInformation("Stopping convention middleware");
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
