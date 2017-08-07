using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMeet.Models
{
    public class Profile
    {
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userEmail { get; set; }
        public string address { get; set; }
        public string userPhoto { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string profile { get; set; }
        public string isVerified { get; set; }
        public int age { get; set; }
        public string about { get; set; }
        public string goalId { get; set; }
        public string goal { get; set; }
        public int countSkill { get; set; }
        public List<SkillModel> skill { get; set; }
        public int countTrainPlace { get; set; }
        public List<Trainplace> trainPlace { get; set; }
        public string hasFacebook { get; set; }
        public Request request { get; set; }
    }

    public class Request
    {
        public int message { get; set; }
        public int friend { get; set; }
    }

   

  

}
