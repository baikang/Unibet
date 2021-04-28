using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bowling.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BowlingScoreController : ControllerBase
    {


        private readonly ILogger<BowlingScoreController> _logger;

        public BowlingScoreController(ILogger<BowlingScoreController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Calculator")]
        public IActionResult Calculator()
        {
            return Ok();
        }
    }
}
