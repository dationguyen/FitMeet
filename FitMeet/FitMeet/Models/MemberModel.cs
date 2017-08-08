using Newtonsoft.Json;
using System.Collections.Generic;

namespace FitMeet.Models
{
    public class Member
    {
        //private List<Activity> activies;
        public string FullName => string.Format("{0} {1}", MemberFirstName, MemberLastName);

        public string NameSort
        {
            get
            {
                if (string.IsNullOrWhiteSpace(MemberFirstName) || MemberFirstName.Length == 0)
                    return "?";

                return MemberFirstName[0].ToString().ToUpper();
            }
        }

        public string GenderLogoUrl
        {
            get
            {
                string url = "";
                if (Gender != null)
                {
                    if (Gender.ToLower() == "male")
                        url = "male.png";
                    else
                    {
                        url = "female.png";
                    }
                }
                return url;
            }
        }

        [JsonProperty("memberId")]
        public string MemberId { get; set; }

        [JsonProperty("memberFirstName")]
        public string MemberFirstName { get; set; }

        [JsonProperty("memberLastName")]
        public string MemberLastName { get; set; }

        [JsonProperty("memberEmail")]
        public string MemberEmail { get; set; }

        [JsonProperty("memberPhoto")]
        public string MemberPhoto { get; set; }

        [JsonProperty("distance")]
        public string Distance { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("about")]
        public string About { get; set; }

        [JsonProperty("goal")]
        public string Goal { get; set; }

        [JsonProperty("isFriend")]
        public string IsFriend { get; set; }

        [JsonProperty("countSkill")]
        public int CountSkill { get; set; }

        [JsonProperty("skill")]
        public List<SkillModel> Skills { get; set; }
    }

}
