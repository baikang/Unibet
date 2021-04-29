using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bowling.Api
{
    public class ScoreResponse
    {
        [JsonProperty("frameProgressScores")]
        public List<string> FrameProgressScores { get; set; }

        [JsonProperty("gameCompleted")]
        public bool IsGameCompleted { get; set; }
    }
}
