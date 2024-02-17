using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace IntegrationTests
{
    public class DummyControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly ApiWebApplicationFactory _factory;

        public DummyControllerTests(ApiWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldAddValidResponseHeaders()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var response = await client.GetAsync("/dummy/response-headers");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Headers.Should().ContainKey("I-CustomHeader");
            response.Headers.Should().ContainKey("C-CustomHeader");
            response.Headers.Should().ContainKey("F-CustomHeader");
            response.Headers.GetValues("I-CustomHeader").Should().Contain("Inline Middleware");
            response.Headers.GetValues("C-CustomHeader").Should().Contain("Convention Middleware");
            response.Headers.GetValues("F-CustomHeader").Should().Contain("Factory Middleware");
        }
    }
}