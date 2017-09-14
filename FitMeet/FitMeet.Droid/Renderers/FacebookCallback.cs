using System;
using Xamarin.Facebook;


namespace FitMeet.Droid.Renderers
{
    class FacebookCallback<TResult>:Java.Lang.Object, IFacebookCallback where TResult : Java.Lang.Object
    {
        public Action<AccessToken> HandleSuccess { get; set; }
        public Action HandleCancel { get; set; }
        public Action<FacebookException> HandleError { get; set; }



        public FacebookCallback()
        {
        }

        public void OnCancel()
        {
            HandleCancel?.Invoke();
        }

        public void OnError(FacebookException error)
        {
            HandleError?.Invoke(error);
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var token = AccessToken.CurrentAccessToken;
            HandleSuccess?.Invoke(token);
        }
    }
}