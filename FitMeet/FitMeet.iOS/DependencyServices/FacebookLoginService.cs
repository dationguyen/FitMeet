using Facebook.LoginKit;
using FitMeet.iOS.DependencyServices;
using FitMeet.Services.DependencyServices;
using System.Linq;
using System.Threading.Tasks;
using UIKit;
[assembly: Xamarin.Forms.Dependency(typeof(FacebookLoginService))]
namespace FitMeet.iOS.DependencyServices
{
    public class FacebookLoginService:IFacebookLoginService
    {
        private LoginManager _loginManager;
        public FacebookLoginService()
        {
            _loginManager = new LoginManager
            {
                LoginBehavior = LoginBehavior.Native
            };
        }

        public async Task<string> LoginAsync(string[] readPermissions)
        {
            var vc = ViewController();
            var result = await _loginManager.LogInWithReadPermissionsAsync(readPermissions,vc);
            return result?.Token.TokenString;
        }

        public async Task<string> LoginAsync()
        {
            string[] readPermissions = { "public_profile" };
            var vc = ViewController();
            var result = await _loginManager.LogInWithReadPermissionsAsync(readPermissions,vc);
            return result?.Token.UserID;
        }

        private UIViewController ViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while(vc.PresentedViewController != null)
                vc = vc.PresentedViewController;

            if(vc is UINavigationController navController)
                vc = navController.ViewControllers.Last();
            return vc;
        }
    }
}