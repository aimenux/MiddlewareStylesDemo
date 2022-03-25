using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using WebApi.Middlewares;
using Xunit;

namespace UnitTests
{
    public class ConventionStyleMiddlewareTests
    {
        [Fact]
        public async Task WhenMiddlewareIsInvokedThenShouldAddValidResponseHeader()
        {
            // arrange
            var logger = NullLogger<ConventionStyleMiddleware>.Instance;
            var context = new DefaultHttpContext
            {
                Response =
                {
                    Body = new MemoryStream()
                }
            };

            RequestDelegate next = _ => Task.CompletedTask;
            var middleware = new ConventionStyleMiddleware(next, logger);

            // act
            await middleware.InvokeAsync(context);

            // assert
            context.Response.Headers.Should().ContainKey("C-CustomHeader");
            context.Response.Headers.Should().ContainValue("Convention Middleware");
        }
    }
}