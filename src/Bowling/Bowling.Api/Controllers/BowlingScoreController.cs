using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bowling.Api
{
    [ApiController]
    [Route("[controller]")]
    public class BowlingScoreController : ControllerBase
    {
        private readonly ILogger<BowlingScoreController> _logger;
        private readonly ICalculatorService _service;

        public BowlingScoreController(ILogger<BowlingScoreController> logger, ICalculatorService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("Calculator")]
        public IActionResult Calculator([FromBody] ScoreRequest payload)
        {
            var scoreResponse = new ScoreResponse();

            if (payload == null || !_service.CalculateScore(payload.PinsDowned, scoreResponse))
            {
                return BadRequest("Incorrect request date or format.");
            }

            return Ok(scoreResponse);
        }
    }
}
