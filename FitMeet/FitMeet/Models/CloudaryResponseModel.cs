using System;

namespace FitMeet.Models
{
    public class CloudaryResponseModel
    {
        public string public_id { get; set; }
        public int version { get; set; }
        public string signature { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string format { get; set; }
        public string resource_type { get; set; }
        public DateTime created_at { get; set; }
        public object[] tags { get; set; }
        public int bytes { get; set; }
        public string type { get; set; }
        public string etag { get; set; }
        public string url { get; set; }
        public string secure_url { get; set; }
        public bool existing { get; set; }
        public string original_filename { get; set; }
    }

}
