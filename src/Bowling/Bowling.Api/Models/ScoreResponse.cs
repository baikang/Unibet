using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bowling.Api
{
    public class ScoreResponse
    {
        [JsonPropertyName("frameProgressScores")]
        public List<string> FrameProgressScores { get; set; }

        [JsonPropertyName("gameCompleted")]
        public bool IsGameCompleted { get; set; }
    }
}
