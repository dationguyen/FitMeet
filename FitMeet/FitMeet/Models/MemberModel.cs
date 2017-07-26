using Newtonsoft.Json;
using System.Collections.Generic;

namespace FitMeet.Models
{
    public class Member
    {
        //private List<Activity> activies;
        public string FullName => string.Format("{0} {1}", MemberFirstName, MemberLastName);

        //public List<Activity> Activities
        //{
        //    get
        //    {
        //        if (activies == null)
        //        {
        //            activies = new List<Activity>();
        //            if (Skills != null)
        //            {
        //                foreach (var skill in Skills)
        //                {
        //                    foreach (var activity in skill?.Activities)
        //                    {
        //                        activies.Add(activity);
        //                    }
        //                }
        //            }
        //        }
        //        return activies;
        //    }
        //}

        public string GenderLogoUrl
        {
            get
            {
                string url = "";
                if (Gender != null)
                {
                    if (Gender == "Male")
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
        public List<Skill> Skills { get; set; }
    }

    public class Skill
    {
        [JsonProperty("levelTitle")]
        public string LevelTitle { get; set; }

        [JsonProperty("LevelId")]
        public int LevelId { get; set; }

        [JsonProperty("countActivity")]
        public int CountActivity { get; set; }

        [JsonProperty("activity")]
        public Activity[] Activities { get; set; }
    }

    public class Activity
    {
        [JsonProperty("activityUserId")]
        public string ActivityUserId { get; set; }

        [JsonProperty("activityId")]
        public string ActivityId { get; set; }

        [JsonProperty("activityIcon")]
        public string ActivityIcon { get; set; }

        [JsonProperty("activityTitle")]
        public string ActivityTitle { get; set; }
    }

}
