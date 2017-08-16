using Newtonsoft.Json;

namespace FitMeet.Models
{
    public class Place
    {
        ~Place()
        {
        }

        //public Place ThisPlace
        //{
        //    get { return this; }
        //    set
        //    {
        //        if ( value != null )
        //        {
        //            this.LocationAddress = value.LocationAddress;
        //            this.LocationId = value.LocationId;
        //            this.LocationTitle = value.LocationTitle;
        //        }
        //    }
        //}

        [JsonProperty("locationAddress")]
        public string LocationAddress { get; set; }

        [JsonProperty("locationId")]
        public string LocationId { get; set; }

        [JsonProperty("locationTitle")]
        public string LocationTitle { get; set; }
    }
}