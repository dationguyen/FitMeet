using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace FitMeet.Services
{
    public class TokenService:ITokenService
    {
        private string _token;

        private const string tokenKey = "Token";
        private ISettings AppSettings =>
            CrossSettings.Current;


        public string GetToken()
        {
            if(_token == null)
            {
                _token = LoadToken();
            }
            return _token;
        }
        public void SetToken(string token)
        {
            if(!String.IsNullOrEmpty(token))
            {
                _token = token;
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
