using FitMeet.iOS.DependencyServices;
using FitMeet.Services.DependencyServices;
using Foundation;


[assembly: Xamarin.Forms.Dependency(typeof(PushNotificationSupportService))]
namespace FitMeet.iOS.DependencyServices
{
    public class PushNotificationSupportService:IPushNotificationSupportService
    {
        public PushNotificationSupportService()
        {

        }
        public string DeviceToken()
        {
            return NSUserDefaults.StandardUserDefaults.StringForKey("PushDeviceToken");
        }
    }
}
