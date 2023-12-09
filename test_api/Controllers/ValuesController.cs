using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace test_api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase {


        private readonly ILogger _logger;
        public TestController(ILogger<TestController> logger) {
            _logger = logger;
        }
        // GET: api/<TestController>
        [HttpGet]
        public IEnumerable<string> Get() {
            var logText = $"hello - {DateTime.Now.ToShortTimeString()}";
            _logger.LogInformation(logText);
            _logger.LogWarning(logText);
            _logger.LogError(logText);
            _logger.LogCritical(logText);
            return new string[] { logText };
        }
    }
} 
