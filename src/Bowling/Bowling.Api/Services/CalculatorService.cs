using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Bowling.Api
{
    public class CalculatorService : ICalculatorService
    {

        private readonly ILogger<CalculatorService> _logger;

        public CalculatorService(ILogger<CalculatorService> logger)
        {
            _logger = logger;
        }

        public bool CalculateScore(List<int> pinsDowned, ScoreResponse scoreResponse)
        {
            return true;
        }

    }
}
