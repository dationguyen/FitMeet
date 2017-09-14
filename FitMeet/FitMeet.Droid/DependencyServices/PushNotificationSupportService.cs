using Android.App;
using Android.Content;
using FitMeet.Droid.DependencyServices;
using FitMeet.Services.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(PushNotificationSupportService))]
namespace FitMeet.Droid.DependencyServices
{
    public class PushNotificationSupportService:IPushNotificationSupportService
    {
        public string DeviceToken()
        {
            //retreive 
            var prefs = Application.Context.GetSharedPreferences("Fitmeet",FileCreationMode.Private);
            var token = prefs.GetString("token",null);
            return token;
        }
     
    }
}