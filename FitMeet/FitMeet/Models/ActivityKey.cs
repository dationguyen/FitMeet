namespace FitMeet.Models
{
    public class ActivityKey
    {
        public Activity Activity { get; set; }
        public Level Level { get; set; }

        public ActivityKey( Activity act , Level lvl )
        {
            Activity = act;
            Level = lvl;
        }
    }

    public class PlaceKey
    {
        public Place Place { get; set; }

        public PlaceKey( Place p )
        {
            Place = p;
        }
    }

}
