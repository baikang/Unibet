using System.Collections.Generic;

namespace Bowling.Api
{
    public interface ICalculatorService
    {
        bool CalculateScore(List<int> pinsDowned, ScoreResponse scoreResponse);
    }
}
