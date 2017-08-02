using System.Collections.Generic;
using Newtonsoft.Json;

namespace FitMeet.Models
{
    class MemberDetail
    {
    }

    public class Rootobject
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("userFirstName")]
        public string UserFirstName { get; set; }

        [JsonProperty("userLastName")]
        public string UserLastName { get; set; }

        [JsonProperty("userEmail")]
        public string UserEmail { get; set; }

        [JsonProperty("userPhoto")]
        public string UserPhoto { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("about")]
        public string About { get; set; }

        [JsonProperty("goal")]
        public string Goal { get; set; }

        [JsonProperty("countSkill")]
        public int CountSkill { get; set; }

        [JsonProperty("skill")]
        public List<SkillModel> Skill { get; set; }

        [JsonProperty("countTrainPlace")]
        public int CountTrainPlace { get; set; }

        [JsonProperty("trainPlace")]
        public List<Trainplace> TrainPlace { get; set; }

        [JsonProperty("isFriend")]
        public string IsFriend { get; set; }
    }



    public class Trainplace
    {
        public string TrainPlaceId { get; set; }
        public string Location { get; set; }
    }

}
