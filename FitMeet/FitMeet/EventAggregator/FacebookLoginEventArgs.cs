using System;

namespace FitMeet.EventAggregator
{
    public class FacebookLoginEventArgs:EventArgs
    {
        public string AccessToken { get; set; }
    }
}
