using Newtonsoft.Json;
using System.Collections.Generic;

namespace FitMeet.Models
{
    public class SkillModel
    {
        [JsonProperty("levelTitle")]
        public string LevelTitle { get; set; }

        [JsonProperty("LevelId")]
        public string LevelId { get; set; }

        [JsonProperty("countActivity")]
        public int CountActivity { get; set; }

        [JsonProperty("activity")]
        public List<ActivityModel> Activities { get; set; }
    }

}
