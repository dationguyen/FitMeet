using Newtonsoft.Json;
using System.Collections.Generic;

namespace FitMeet.Models
{
    public class ActivityData
    {
        [JsonProperty("activity")]
        public List<Activity> Activity { get; set; }

        [JsonProperty("skill")]
        public List<Level> Skill { get; set; }

        [JsonProperty("goal")]
        public List<Goal> Goal { get; set; }
    }
}
