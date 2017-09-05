using FitMeet.iOS.DependencyServices;
using FitMeet.Services.DependencyServices;
using Foundation;


[assembly: Xamarin.Forms.Dependency(typeof(GetDeviceTokenService))]
namespace FitMeet.iOS.DependencyServices
{
    public class GetDeviceTokenService:IGetDeviceTokenService
    {
        public GetDeviceTokenService()
        {

        }
        public string DeviceToken()
        {
            return NSUserDefaults.StandardUserDefaults.StringForKey("PushDeviceToken");
        }
    }
}
