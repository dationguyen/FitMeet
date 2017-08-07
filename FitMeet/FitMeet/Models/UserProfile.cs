using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FitMeet.Models
{
    public class UserProfile
    {
        [JsonProperty("userFirstName")]
        public string UserFirstName { get; set; }

        [JsonProperty("userLastName")]
        public string UserLastName { get; set; }

        [JsonProperty("userEmail")]
        public string UserEmail { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("userPhoto")]
        public string UserPhoto { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("dob")]
        public string Dob { get; set; }

        [JsonProperty("profile")]
        public string Profile { get; set; }

        [JsonProperty("isVerified")]
        public string IsVerified { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("about")]
        public string About { get; set; }

        [JsonProperty("goalId")]
        public string GoalId { get; set; }

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

        [JsonProperty("hasFacebook")]
        public string HasFacebook { get; set; }

        [JsonProperty("request")]
        public Request Request { get; set; }
    }

    public class Request
    {
        [JsonProperty("message")]
        public int Message { get; set; }

        [JsonProperty("friend")]
        public int Friend { get; set; }
    }

}



