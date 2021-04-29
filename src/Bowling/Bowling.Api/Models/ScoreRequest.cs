using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bowling.Api
{
    public class ScoreRequest
    {
        [JsonProperty("pinsDowned")]
        public List<int> PinsDowned { get; set; }
    }
}