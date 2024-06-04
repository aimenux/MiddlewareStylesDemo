using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using WebApi.Middlewares;
using Xunit;

namespace UnitTests;

public class FactoryStyleMiddlewareTests
{
    [Fact]
    public async Task WhenMiddlewareIsInvokedThenShouldAddValidResponseHeader()
    {
        // arrange
        using var body = new MemoryStream();
        var logger = NullLogger<FactoryStyleMiddleware>.Instance;
        var context = new DefaultHttpContext
        {
            Response =
            {
                Body = body
            }
        };

        RequestDelegate next = _ => Task.CompletedTask;
        var middleware = new FactoryStyleMiddleware(logger);

        // act
        await middleware.InvokeAsync(context, next);

        // assert
        context.Response.Headers.Should().ContainKey("F-CustomHeader");
        context.Response.Headers.Should().ContainValue("Factory Middleware");
    }
}