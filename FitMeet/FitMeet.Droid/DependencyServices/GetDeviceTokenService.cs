using FitMeet.Droid.DependencyServices;
using FitMeet.Services.DependencyServices;
[assembly: Xamarin.Forms.Dependency(typeof(GetDeviceTokenService))]
namespace FitMeet.Droid.DependencyServices
{
    public class GetDeviceTokenService:IGetDeviceTokenService
    {
        public string DeviceToken()
        {
            return "";
        }
    }
}