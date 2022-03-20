using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DummyController : ControllerBase
    {
        private readonly ILogger<DummyController> _logger;

        public DummyController(ILogger<DummyController> logger)
        {
            _logger = logger;
        }

        [HttpGet("response-headers")]
        public IDictionary<string, string> GetResponseHeaders()
        {
            var requestHeaders =  new Dictionary<string, string>();
            foreach (var (key, value) in Response.Headers.OrderBy(x => x.Key))
            {
                requestHeaders.Add(key, value);
            }
            return requestHeaders;
        }
    }
}