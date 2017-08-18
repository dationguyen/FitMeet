using Plugin.SecureStorage;
using System;

namespace FitMeet.Services
{
    public class TokenServices:ITokenServices
    {
        private IFitMeetRestService _fitMeetRestService;
        private string _token;

        private const string tokenKey = "Token";

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

        public bool HasValidToken()
        {
            return true;
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
            CrossSecureStorage.Current.DeleteKey(tokenKey);
        }

        private void SaveToken(string token)
        {
            CrossSecureStorage.Current.SetValue(tokenKey,token);
        }

        private string LoadToken()
        {
            return CrossSecureStorage.Current.GetValue(tokenKey);
        }
    }
}
