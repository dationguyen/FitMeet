using FitMeet.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public class FacebookService:IFacebookService
    {
        private string baseUrl = "https://graph.facebook.com/v2.10/";

        public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
        {
            var requestParam =
                "me?fields=email,picture,first_name,last_name,name&access_token="
                + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(baseUrl + requestParam);

            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

            return facebookProfile;
        }
    }
}
