using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FitMeet.Models
{
    public class MemberDetail
    {
        public string FullName
        {
            get => string.Format("{0} {1}" , UserFirstName , UserLastName);
          
        }


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
}
