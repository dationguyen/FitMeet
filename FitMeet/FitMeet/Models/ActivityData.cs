using Newtonsoft.Json;
using System.Collections.Generic;

namespace FitMeet.Models
{
    public class ActivityData
    {
        [JsonProperty("activity")]
        public List<Activity> Activity { get; set; }

        [JsonProperty("skill")]
        public List<Skill> Skill { get; set; }

        [JsonProperty("goal")]
        public List<Goal> Goal { get; set; }
    }

    public class Activity
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("icon_white")]
        public string Icon_white { get; set; }
    }

    public class Skill
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Goal
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
