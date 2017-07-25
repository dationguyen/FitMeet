using System.Net.Http;

namespace FitMeet.Services

{
    public class FitMeetRestService : RestApiClient, IFitMeetRestService
    {
        public FitMeetRestService()
        {
            this.httpClient = new HttpClient() { };
        }



    }
}
