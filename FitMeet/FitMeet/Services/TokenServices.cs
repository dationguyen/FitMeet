using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public class TokenServices:ITokenServices
    {
        private IFitMeetRestService _fitMeetRestService;
        private string _token;

        private const string tokenKey = "Token";
        private ISettings AppSettings =>
            CrossSettings.Current;

        public TokenServices(IFitMeetRestService fitMeetRestService)
        {
            _fitMeetRestService = fitMeetRestService;
        }

        public string GetToken()
        {
            if(_token == null)
            {
                _token = LoadToken();
            }
            return _token;
        }

        public async Task<bool> HasValidToken()
        {
            bool valid = await _fitMeetRestService.CheckTokenAsync(_token);
            return valid;
        }

        public void SetToken(string token)
        {
            if(!String.IsNullOrEmpty(token))
            {
                _token = token;
                _fitMeetRestService.SetToken(token);
                SaveToken(_token);
            }
            else
            {
                RemoveToken();
            }
        }

        private void RemoveToken()
        {
            AppSettings.Remove(tokenKey);
        }

        private void SaveToken(string token)
        {
            AppSettings.AddOrUpdateValue(tokenKey,token);
        }

        private string LoadToken()
        {
            return AppSettings.GetValueOrDefault(tokenKey,string.Empty);
        }
    }
}
