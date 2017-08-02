using System.Collections.Generic;

namespace FitMeet.EventAggregator
{
    public class UpdateFilterEventArgs
    {
        public bool IsMale { get; set; }
        public int Distance { get; set; }

        public List<int> Activities { get; set; }
        public UpdateFilterEventArgs(int distance, bool isMale, List<int> activities)
        {
            IsMale = isMale;
            Distance = distance;
            Activities = activities;
        }
    }
}
