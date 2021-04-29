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
            if (pinsDowned == null || pinsDowned.Count < Rolls.Min || pinsDowned.Count > Rolls.Max)
            {
                return false;
            }

            try
            {
                var game = new Game(pinsDowned);
                scoreResponse.FrameProgressScores = game.Frames.Select(x => (x.Score == Game.UnDeterminedScore) ? "*" : x.Score.ToString()).ToList();
                scoreResponse.IsGameCompleted = game.IsGameCompleted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return false;
            }

            return true;
        }

    }
}
