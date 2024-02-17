namespace WebApi.Middlewares;

public enum InlineStyleMiddleware
{
    Use,
    UseWhen,
    Run,
    Map,
    MapWhen
}

public static class InlineStyleMiddlewareExtensions
{
    public static void UseInlineStyleMiddleware(this WebApplication app, InlineStyleMiddleware style = InlineStyleMiddleware.Use)
    {
        switch (style)
        {
            case InlineStyleMiddleware.Use:
                app.Use(async (context, next) =>
                {
                    app.Logger.LogInformation("Starting inline middleware");
                    context.Response.Headers.TryAdd("I-CustomHeader", "Inline Middleware");
                    await next(context);
                    app.Logger.LogInformation("Stopping inline middleware");
                });
                break;
            case InlineStyleMiddleware.UseWhen:
                app.UseWhen(c => c.Request.Query.ContainsKey("usewhen"),
                    a => a.Use(async (context, next) =>
                    {
                        app.Logger.LogInformation("Starting inline middleware");
                        context.Response.Headers.TryAdd("I-CustomHeader", "Inline Middleware");
                        await next(context);
                        app.Logger.LogInformation("Stopping inline middleware");
                    }));
                break;
            case InlineStyleMiddleware.Run:
                app.Run(async context =>
                {
                    app.Logger.LogInformation("Starting inline middleware");
                    context.Response.Headers.TryAdd("I-CustomHeader", "Inline Middleware");
                    await context.Response.WriteAsync("Run");
                    app.Logger.LogInformation("Stopping inline middleware");
                });
                break;
            case InlineStyleMiddleware.Map:
                app.Map("/map",
                    a => a.Run(async context =>
                    {
                        app.Logger.LogInformation("Starting inline middleware");
                        context.Response.Headers.TryAdd("I-CustomHeader", "Inline Middleware");
                        await context.Response.WriteAsync("Map");
                        app.Logger.LogInformation("Stopping inline middleware");
                    }));
                break;
            case InlineStyleMiddleware.MapWhen:
                app.MapWhen(c => c.Request.Query.ContainsKey("mapwhen"),
                    a => a.Run(async context =>
                    {
                        app.Logger.LogInformation("Starting inline middleware");
                        context.Response.Headers.TryAdd("I-CustomHeader", "Inline Middleware");
                        await context.Response.WriteAsync("MapWhen");
                        app.Logger.LogInformation("Stopping inline middleware");
                    }));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(style), style, "Unexpected inline style");
        }
    }
}